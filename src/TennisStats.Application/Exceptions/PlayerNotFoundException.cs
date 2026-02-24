namespace TennisStats.Application.Exceptions
{
    public class PlayerNotFoundException : Exception
    {
        public PlayerNotFoundException(int id) 
            : base($"Le joueur avec l'ID {id} n'a pas été trouvé.")
        {
        }
    }
}
