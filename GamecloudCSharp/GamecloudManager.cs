using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using RestSharp;

namespace Gamecloud
{
    /// <summary>
    /// Gamecloud Manager handles all the connections & main functions related to Gamecloud
    /// </summary>
    public sealed class GamecloudManager
    {
        private string GAMECLOUD_ADDRESS = "";
        private string AUTH_TOKEN = "NO_AUTH";

        private static volatile GamecloudManager instance;
        private static object syncRoot = new object();

        // The private constructor
        private GamecloudManager() {}

        /// <summary>
        /// Initializes the required parameters for Gamecloud manager
        /// </summary>
        /// <param name="address">The address of the gamecloud to use</param>
        /// <param name="auth">The developer authentication token</param>
        public void Initialize(string address, string auth)
        {
            this.GAMECLOUD_ADDRESS = address;
            this.AUTH_TOKEN = auth;
        }

        // The public hash libraries
        //The list of all dictionaries for storing different information
        public Dictionary<string, string> GetItemsDict = new Dictionary<string, string>();
        public Dictionary<string, string> GainItemsDict = new Dictionary<string, string>();
        public Dictionary<string, string> LoseItemsDict = new Dictionary<string, string>();
        public Dictionary<string, string> AskAchievementsDict = new Dictionary<string, string>();
        public Dictionary<string, string> GiveAchievementsDict = new Dictionary<string, string>();
        public Dictionary<string, string> TriggerEventsDict = new Dictionary<string, string>();
        public Dictionary<string, string> HasTriggeredEventsDict = new Dictionary<string, string>();

        
		// CallTypes, that can be used (in some situations, currently unused)
		public enum callTypes {
			GAIN_ITEM,
			ASK_ITEM,
			LOSE_ITEM,
			TRIGGER_EVENT,
			ASK_EVENT,
			GAIN_ACHIEVEMENT,
			ASK_ACHIEVEMENT
		};

		/// <summary>
		/// Creates the Gamecloud call for asking data in a proper JSON format
		/// </summary>
		/// <returns>The proper Dictionary to send to Gamecloud</returns>
		/// <param name="hash">The hash for the given query</param>
		/// <param name="playerId">Player identifier.</param>
		/// <param name="characterId">Character identifier.</param>
		protected Dictionary<string, string> createCall(string hash, string playerId, string characterId) 
		{
            // Create the Dictionary
            Dictionary<string, string> data = new Dictionary<string, string>();

			// Create the callType
			data.Add("callType", "gameDataSave");
			// Add the authkey
			data.Add("authkey", AUTH_TOKEN);
			// Add the hash
			data.Add("hash", hash);

			// If there is playerId
            if (playerId != null)
            {
                // Add the player ID
                data.Add("playerId", playerId);
            }
            // Otherwise, make it just empty string
            else
            {
                data.Add("playerId", "");
            }
			// If the characterId exists
            if (characterId != null)
            {
                // Add the character ID
                data.Add("characterId", characterId);
            }
            // Otherwise, make an empty string
            else
            {
                data.Add("characterId", "");
            }

			// Then, return the data
			return data;
		}

		/// <summary>
		/// Creates the call for Asking if player has items
		/// </summary>
		/// <param name="hash">ASK hash</param>
		/// <param name="playerId">Player identifier</param>
		/// <param name="characterId">Character identifier</param>
        /// <param name="callback">The callback function</param>
        public void askItems(string hash, string playerId, string characterId, Action<IRestResponse> callback)
		{

			// Create the call
			Dictionary<string, string> data = createCall(hash, playerId, characterId);
			// Send the data to Gamecloud
			SendData(data, callback);
		}

		/// <summary>
		/// Creates the call for giving a gainItem event for the player
		/// </summary>
		/// <param name="hash">gainItem hash</param>
		/// <param name="playerId">Player identifier.</param>
		/// <param name="characterId">Character identifier.</param>
        /// <param name="callback">The callback function</param>
        public void gainItem(string hash, string playerId, string characterId, Action<IRestResponse> callback) 
		{
			// Create the call
			Dictionary<string, string> data = createCall(hash, playerId, characterId);
			// Send the data to Gamecloud
            SendData(data, callback);
		}

		/// <summary>
		/// Creates the call for giving a loseItem event for the player
		/// </summary>
		/// <param name="hash">loseItem hash.</param>
		/// <param name="playerId">Player identifier.</param>
		/// <param name="characterId">Character identifier.</param>
        /// <param name="callback">The callback function</param>
        public void loseItem(string hash, string playerId, string characterId, Action<IRestResponse> callback)
		{
			// Create the call
			Dictionary<string, string> data = createCall(hash, playerId, characterId);
			// Send the data to Gamecloud
            SendData(data, callback);
		}

		/// <summary>
		/// Creates the triggerEvent call for gamecloud
		/// </summary>
		/// <param name="hash">triggerEvent hash.</param>
		/// <param name="playerId">Player identifier.</param>
        /// <param name="callback">The callback function</param>
        public void triggerEvent(string hash, string playerId, Action<IRestResponse> callback)
		{
			// Create the call, no need for characterId so it is null
			Dictionary<string, string> data = createCall(hash, playerId, null);
			// Send the data to Gamecloud
            SendData(data, callback);
		}

		/// <summary>
		/// Asks if the player in question has received the event in question
		/// </summary>
		/// <param name="hash">askEvent hash.</param>
		/// <param name="playerId">Player identifier.</param>
        /// <param name="callback">The callback function</param>
        public void askEvent(string hash, string playerId, Action<IRestResponse> callback)
		{
			// Create the call, no need for characterId so it is null
			Dictionary<string, string> data = createCall(hash, playerId, null);
			// Send the data to Gamecloud
            SendData(data, callback);
		}

		/// <summary>
		/// Creates the query for giving a gainAchievement event for the player
		/// </summary>
		/// <param name="hash">GainAchievememt hash.</param>
		/// <param name="playerId">Player identifier.</param>
        /// <param name="callback">The callback function</param>
        public void gainAchievement(string hash, string playerId, Action<IRestResponse> callback) 
		{
			// Create the call, no need for characterId so it is null
			Dictionary<string, string> data = createCall(hash, playerId, null);
			// Send the data to Gamecloud
            SendData(data, callback);
		}

		/// <summary>
		/// Asks if the player has received the Achievement in question
		/// </summary>
		/// <param name="hash">ASK Achievement hash.</param>
		/// <param name="playerId">Player identifier.</param>
        /// <param name="callback">The callback for the network call</param>
        public void askAchievement(string hash, string playerId, Action<IRestResponse> callback)
		{
			// Create the call, no need for characterId so it is null
            Dictionary<string, string> data = createCall(hash, playerId, null);
			// Send the data to Gamecloud
            SendData(data, callback);
		}


		/// <summary>
		/// Sends the data in proper JSON format to the Gamecloud
		/// </summary>
		/// <param name="data">
		/// The properly formated data, done by using the createCall function with proper information.
		/// </param>
		public void SendData(Dictionary<string, string> data, Action<IRestResponse> callback) 
		{
            // Create the client
            var client = new RestClient();
            client.BaseUrl = GAMECLOUD_ADDRESS;

            // Create the request and set the format properly
            var request = new RestRequest(Method.POST) ;
            request.RequestFormat = DataFormat.Json;

             // Add the data to the request body
            request.AddBody(new { 
                callType = data["callType"], 
                authkey = data["authkey"], 
                hash = data["hash"], 
                playerId = data["playerId"], 
                characterId = data["characterId"] });


            // Execute the request in Async and send results to the callback
            client.ExecuteAsync(request, callback);
           

		} // End of SendData()

        /// <summary>
        /// Thread safe singleton based on http://msdn.microsoft.com/en-us/library/ff650316.aspx
        /// Will return the instance object of the singleton class or create it, if it does not exist
        /// </summary>
        public static GamecloudManager Instance
        {
            get
            {
                // If instance is null
                if (instance == null)
                {
                    // Lock the syncRoot
                    lock (syncRoot)
                    {
                        // If instance is still null
                        if (instance == null)
                        {
                            // Instantiate the class
                            instance = new GamecloudManager();
                        }
                    }
                }

                // Return the singleton instance
                return instance;

            } // End of get
        } // End of Instace()

    } // End of class Gamecloud

} // End of namespace Gamecloud