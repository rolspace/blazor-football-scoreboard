using System.ComponentModel.DataAnnotations.Schema;

namespace Football.Core.Persistence.MySql.Entities
{
    public partial class PlayEntity
    {
        [NotMapped]
        protected internal bool IsHomeTeamOnOffense => Posteam == HomeTeam;

        [NotMapped]
        protected internal bool IsAwayTeamOnOffense => !IsHomeTeamOnOffense;

        [NotMapped]
        protected internal bool IsHomeTeamReceivingKickoffOrPunt
        {
            get
            {
                bool isHomeTeamReceivingKickoff = PlayType == "kickoff" && Posteam == HomeTeam;
                bool isHomeTeamReceivingPunt = PlayType == "punt" && Defteam == HomeTeam;

                return isHomeTeamReceivingKickoff || isHomeTeamReceivingPunt;
            }
        }

        [NotMapped]
        protected internal bool IsAwayTeamReceivingKickoffOrPunt => !IsHomeTeamReceivingKickoffOrPunt;
    }
}
