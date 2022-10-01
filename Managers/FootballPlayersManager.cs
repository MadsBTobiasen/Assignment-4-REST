using Assignment1CAndUnitTests;

namespace Assignment_4_REST.Managers
{
    public class FootballPlayersManager
    {

        #region Fields
        /// <summary>
        /// A static int, to allow for unique ids to be assigned to the FootballPlayers.
        /// </summary>
        private static int _nextID = 1;
        /// <summary>
        /// A static list of FootballPlayer(s), to be initiated in the constructor, to make use of the Add()-Method, to
        /// ensure that each FootballPlayer gets assigned a unique id.
        /// </summary>
        private static List<FootballPlayer> footballPlayersList => footballPlayersDictionary.Values.ToList();
        /// <summary>
        /// A static Dictionary of FootballPlayer(s), to be initiated in the constructor, to make use of the Add()-Method, to
        /// ensure that each FootballPlayer gets assigned a unique id.
        /// A Dictionary instead of a list, is to ensure easy lookup in the dictionary, through the use of the ContainsKey()-Method,
        /// such that when trying to get an object from the collection, you don't have to itterate through the entire list.
        /// </summary>
        private static Dictionary<int, FootballPlayer> footballPlayersDictionary = new Dictionary<int, FootballPlayer>();
        #endregion

        #region Properties

        #endregion

        #region Constructor
        public FootballPlayersManager()
        {

            #region Creating the static list.
            /*
             * Due to the Manager getting initiated each time a new call to the REST-Api is made,
             * a check to see if the Dictionary hasn't been initiated is made, to ensure that the data isn't overriden
             * or replaced, FootBallPlayer each time the Manager is initiated.
            */
            if (footballPlayersDictionary == null || footballPlayersDictionary.Count <= 0)
            {
                
                footballPlayersDictionary = new Dictionary<int, FootballPlayer>();
                
                //Goalkeepers
                Add(new FootballPlayer() { Name = "Mads Hermansen", Age = 22, ShirtNumber = 1 });

                //Defenders
                Add(new FootballPlayer() { Name = "Jens-Martin Gammelby", Age = 27, ShirtNumber = 28 });
                Add(new FootballPlayer() { Name = "Andreas Maxsø", Age = 28, ShirtNumber = 5 });
                Add(new FootballPlayer() { Name = "Kevin Tshiembe", Age = 25, ShirtNumber = 18 });
                Add(new FootballPlayer() { Name = "Blas Riveros", Age = 24, ShirtNumber = 15 });

                //Midfielders
                Add(new FootballPlayer() { Name = "Josip Radosevic", Age = 28, ShirtNumber = 22 });
                Add(new FootballPlayer() { Name = "Mathias Greve", Age = 27, ShirtNumber = 8 });
                Add(new FootballPlayer() { Name = "Daniel Wass", Age = 33, ShirtNumber = 10 });
                Add(new FootballPlayer() { Name = "Nicolai Vallys", Age = 26, ShirtNumber = 7 });

                //Forwards
                Add(new FootballPlayer() { Name = "Simon Hedlund", Age = 29, ShirtNumber = 27 });
                Add(new FootballPlayer() { Name = "Ohi Omoijuanfo", Age = 28, ShirtNumber = 9 });

            }
            #endregion

        }
        #endregion

        #region Methods
        /// <summary>
        /// Method that returns the list of all the FootballPlayers in the manager.
        /// A new list is created, such that the original list isn't returned.
        /// </summary>
        /// <returns>List of FootballPlayers</returns>
        public List<FootballPlayer> GetAll()
        {
            return new List<FootballPlayer>(footballPlayersList);
        }

        /// <summary>
        /// Method that tries to find a FootballPlayer in the dictionary, with the given id in the argument.
        /// If a match is found, the match is returned, if no match was found, null is returned.
        /// </summary>
        /// <param name="id">Id to match with.</param>
        /// <returns>FootballPlayer-object, or null.</returns>
        /// <exception cref="KeyNotFoundException">An exception thrown if no footballPlayer with given id exists.</exception>
        public FootballPlayer GetByID(int id)
        {
            if (footballPlayersDictionary.ContainsKey(id))
                return footballPlayersDictionary[id];

            throw new KeyNotFoundException($"FootballPlayer with id: ({id}) was not found.");

        }

        /// <summary>
        /// Method that adds the player-argument to the dictionary. Also ensures that the added player, gets assigned
        /// a unique id, before adding it to the collection.
        /// </summary>
        /// <param name="player">FootballPlayer object to be added.</param>
        /// <returns>The added FootballPlayer object, with an assigned id.</returns>
        public FootballPlayer Add(FootballPlayer player)
        {

            //While loop to ensure that no new added objects, has a duplicate id.
            while (footballPlayersDictionary.ContainsKey(_nextID)) _nextID++;

            if (!footballPlayersDictionary.ContainsKey(_nextID))
            {

                //Assign the unique id to the player.
                player.ID = _nextID++;

                //Run the Validator-Method after the id has been assigned, as an exception can be thrown
                //if the id has not been set.
                player.Validate();

                footballPlayersDictionary.Add(player.ID, player);

                return player;

            }

            throw new Exception("Object wasn't added.");

        }

        /// <summary>
        /// Method that tries to update the dictionary with a new FootballPlayer. If a FootballPlayer is found, the entry in the dictionary
        /// is overriden with the footballPlayer from the request, if no match for the id is found, an exception is thrown.
        /// </summary>
        /// <param name="id">Id of the footballPlayer to update.</param>
        /// <param name="footballPlayer">FootballPlayer-object to update with.</param>
        /// <returns>Updated FootbalLPlayer-object.</returns>
        /// <exception cref="KeyNotFoundException">An exception thrown if no footballPlayer with given id exists.</exception>
        public FootballPlayer Update(int id, FootballPlayer footballPlayer)
        {

            if(footballPlayersDictionary.ContainsKey(id))
            {

                //First we assign the id from the request, to the object, such that the Validator-Method can be run.
                footballPlayer.ID = id;
                footballPlayer.Validate();

                //Then we update the Dictionary.
                footballPlayersDictionary[id] = footballPlayer;

                return footballPlayersDictionary[id];

            }

            throw new KeyNotFoundException($"FootballPlayer with id: ({id}) was not found.");

        }

        /// <summary>
        /// Method that tries to remove a FootballPlayer from the dictionary, with a matching id.
        /// </summary>
        /// <param name="id">Id to match for.</param>
        /// <returns>Deleted FootballPlayers-object.</returns>
        /// <exception cref="KeyNotFoundException">An exception thrown if no FootballPlayer is found with given id.</exception>
        public FootballPlayer Delete(int id)
        {

            if (footballPlayersDictionary.ContainsKey(id))
            {
                FootballPlayer footballPlayer = footballPlayersDictionary[id];
                footballPlayersDictionary.Remove(id);

                return footballPlayer;

            }

            throw new KeyNotFoundException($"FootballPlayer with id: ({id}) was not found.");

        }
        #endregion

    }
}
