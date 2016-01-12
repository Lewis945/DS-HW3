using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatchMeUp.Core.Game
{
    public interface IGameSession
    {
        Guid Id { get; set; }

        string SessionName { get; set; }
        string SessionCreator { get; set; }

        int JoinedPlayersNumber { get; set; }

        int FieldHeight { get; set; }
        int FieldWidth { get; set; }
    }
}
