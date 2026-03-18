# Plan: Hub Connection Failure Notification

## Context

The Index page silently ignores `HubException` — it logs the error but leaves the user staring at a loading spinner indefinitely. Per requirements in `docs/requirements/001-hub-connection-failure-notification.md`, the page must instead show a distinct notification with a full-page reload button when the Hub connection fails, without exposing exception details to the user.

---

## Files to Modify

| File | Change |
|---|---|
| `src/Hosts/Blazor/Pages/Index.razor` | Add `hubConnectionFailed` state, hub failure UI, reload logic |
| `tests/Football.Blazor.UnitTests/Pages/IndexTests.cs` | Add 4 new tests covering hub failure scenarios |

---

## Implementation

### 1. `Index.razor` — new state field

In `@code`, add below `playsToUpdate`:
```csharp
private bool hubConnectionFailed = false;
```

### 2. `Index.razor` — update `HubException` catch block

Change the existing catch that only logs, to also set state:
```csharp
catch (HubException ex)
{
    hubConnectionFailed = true;
    Logger.LogError(ex, "Index page: There was an error initializing the Hub connection.");
}
```

### 3. `Index.razor` — update `RetryLoadGames` reset

Add reset for the new flag so Retry clears both failure states:
```csharp
private async Task RetryLoadGames()
{
    loadFailed = false;
    hubConnectionFailed = false;
    playsToUpdate = new List<PlayDto>();
    await OnInitializedAsync();
}
```

### 4. `Index.razor` — add hub failure notification block (above the existing if/else chain)

The hub notification must render **independently** of the game cards so it shows even when games loaded successfully (Req 1.3). Place it before the `@if (playsToUpdate.Count() > 0)` block:

```razor
@if (hubConnectionFailed)
{
    <div class="mdc-layout-grid__cell mdc-layout-grid__cell--span-8-tablet mdc-layout-grid__cell--span-12-desktop">
        <div style="text-align: center; padding: 20px;">
            <p class="mdc-typography--body1">An error occurred. Live score updates are unavailable.</p>
            <button class="mdc-button mdc-button--raised" @onclick="ReloadPage">
                <span class="mdc-button__label">Reload</span>
            </button>
        </div>
    </div>
}
```

### 5. `Index.razor` — update spinner condition

Prevent the loading indicator from showing when hub failed (Req 1.2):
```razor
else if (!loadFailed && !hubConnectionFailed)
{
    // spinner markup (unchanged)
}
```

### 6. `Index.razor` — add `ReloadPage` method

`NavigationManager` is already injected in `GameComponentBase`. Use it for a full-page reload (Req 2.2):
```csharp
private void ReloadPage()
{
    NavigationManager.NavigateTo(NavigationManager.Uri, forceLoad: true);
}
```

---

## Final UI Rendering Logic (summary)

```
[Hub failure block] ← independent, shows regardless of game load state

@if (playsToUpdate.Count() > 0)          → show game cards
else if (!loadFailed && !hubConnectionFailed) → show spinner
else if (loadFailed)                     → show "games could not be loaded" + Retry
```

---

## Tests to Add in `IndexTests.cs`

### Test 1: `Index_HubConnectionFails_DisplaysHubErrorNotification`
- Setup: API returns games successfully; `mockHubFactory.Setup(x => x.CreateHub()).Throws(new HubException(...))`
- Assert: notification text "Live score updates are unavailable" is present in markup

### Test 2: `Index_HubConnectionFails_DoesNotDisplayLoadingIndicator`
- Setup: same as Test 1
- Assert: `mdc-circular-progress` not present in markup

### Test 3: `Index_HubConnectionFails_WithGamesLoaded_DisplaysBothGamesAndNotification`
- Setup: API returns games successfully; hub factory throws HubException
- Assert: game cards rendered **and** hub notification visible (Req 1.3)

### Test 4: `Index_HubErrorReloadButton_TriggersPageNavigation`
- Setup: same hub failure setup
- Assert: after clicking the Reload button, `FakeNavigationManager.History` contains a navigation entry with the base URI and `forceLoad: true`

**Hub failure setup pattern** (avoids retry delays):
```csharp
mockHubFactory.Setup(x => x.CreateHub()).Throws(new HubException("connection failed"));
```
This throws immediately in `InitializeHubConnection` before `StartAsync` is reached, bypassing Polly retries and keeping tests fast.

**FakeNavigationManager verification pattern** (bUnit built-in):
```csharp
var navManager = Services.GetRequiredService<FakeNavigationManager>();
navManager.History.Should().ContainSingle(e => e.Uri == navManager.Uri && e.Options.ForceLoad == true);
```

---

## Verification

1. Run `dotnet test tests/Football.Blazor.UnitTests/` — all existing + 4 new tests pass
2. Manually run the app with a misconfigured Hub URL and confirm:
   - The notification appears at the top of the page
   - No spinner is visible
   - The Reload button performs a full browser refresh
   - Game cards still render if the API call succeeded before the hub failed
