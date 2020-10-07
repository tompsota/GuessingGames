using System.ComponentModel;

namespace GuessingGame
{
    /// <summary>
    /// Info about current player
    /// </summary>
    public class Player
    {
        [DisplayName("Player")]
        public string PlayerName { get; set; }
        [DisplayName("Score")]
        public int Points { get; set; }

    }
}
