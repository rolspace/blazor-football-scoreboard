# Requirements Document

## Introduction

When the Index page loads and the SignalR Hub connection fails to initialize, the user currently receives no feedback — the page silently loads without live scores and the error is only logged. This feature provides the user with a visible notification and a button to reload the page so they can recover from the failure without manual browser intervention.

## Requirements

### Requirement 1

**User Story:** As a user, when the Index page loads and the Hub connection initialization fails, I want to see a notification on the page, so that I know live scores are unavailable.

#### Acceptance Criteria

1. WHEN the Hub connection initialization throws a `HubException` THEN the system SHALL display a visible error message on the Index page informing the user that the live score connection failed.
2. WHEN the Hub connection initialization throws a `HubException` THEN the system SHALL NOT display the loading indicator.
3. WHEN game data loads successfully but the Hub connection fails THEN the system SHALL still display the notification, since live updates will not be received.

### Requirement 2

**User Story:** As a user, when the Hub connection failure notification is displayed, I want to see a button to reload the page, so that I can attempt to reconnect without manually refreshing the browser.

#### Acceptance Criteria

1. WHEN the Hub connection failure notification is displayed THEN the system SHALL show a reload button alongside the notification message.
2. WHEN the user clicks the reload button THEN the system SHALL reload the page from scratch (full page navigation), resetting all state and reattempting both the game data load and Hub connection initialization.

## Non-Functional Requirements

### Code Architecture and Modularity
- Hub connection failure state must be tracked separately from general load failure state (`loadFailed`) so that each failure type can render a distinct UI.

### Performance
- No additional network requests should be made as a result of displaying the notification.

### Security
- Error details from the `HubException` SHALL NOT be exposed to the user; only a generic, user-friendly message SHALL be displayed.

### Reliability
- The reload button SHALL trigger a full page reload to ensure a clean state.
- The notification SHALL be displayed even when game data was successfully fetched before the Hub connection failed.

### Usability
- The notification message and reload button SHALL follow existing UI patterns (MDC typography and button styles) consistent with the existing error state on the Index page.
- The notification SHALL be clearly visible without requiring the user to scroll.
