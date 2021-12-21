using System.ComponentModel.DataAnnotations.Schema;

namespace Football.Core.Persistence.MySql.Entities
{
    public partial class PlayEntity
    {
        [NotMapped]
        protected internal bool IsHomeTeamOnOffense => Posteam == HomeTeam;

        [NotMapped]
        protected internal bool IsAwayTeamOnOffense => Posteam == AwayTeam;

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
        protected internal bool IsAwayTeamReceivingKickoffOrPunt
        {
            get
            {
                bool isAwayTeamReceivingKickoff = PlayType == "kickoff" && Posteam == AwayTeam;
                bool isAwayTeamReceivingPunt = PlayType == "punt" && Defteam == AwayTeam;

                return isAwayTeamReceivingKickoff || isAwayTeamReceivingPunt;
            }
        }

        [NotMapped]
        protected internal bool IsHomeTeamPunting
        {
            get
            {
                return PlayType == "punt" && Posteam == HomeTeam;
            }
        }

        [NotMapped]
        protected internal bool IsAwayTeamPunting
        {
            get
            {
                return PlayType == "punt" && Posteam == AwayTeam;
            }
        }
    }
}
