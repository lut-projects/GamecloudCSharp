using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RestSharp;
using RestSharp.Deserializers;

namespace Gamecloud
{
    /// <summary>
    /// A class for containing gamecloud callbacks
    /// </summary>
    public class GamecloudCallbacks
    {
        // The Singleton-required instance stuff
        private static volatile GamecloudCallbacks instance;
        private static object syncRoot = new object();

        // The private constructor
        private GamecloudCallbacks() { }

        /// <summary>
        /// Thread safe singleton based on http://msdn.microsoft.com/en-us/library/ff650316.aspx
        /// Will return the instance object of the singleton class or create it, if it does not exist
        /// </summary>
        public static GamecloudCallbacks Instance
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
                            instance = new GamecloudCallbacks();
                        }
                    }
                }

                // Return the singleton instance
                return instance;

            } // End of get
        } // End of Instace()

        /// <summary>
        /// Handles the Get Item Response from the Gamecloud
        /// </summary>
        /// <param name="response">The response received from the Gamecloud</param>
        public void HandleItemGetResponse(IRestResponse response)
        {
            JsonDeserializer deserializer = new JsonDeserializer();
            ItemGamecloud result = deserializer.Deserialize<ItemGamecloud>(response);
        }

        /// <summary>
        /// Handles the Get Achievement Response from the Gamecloud
        /// </summary>
        /// <param name="response">The response received from the Gamecloud</param>
        public void HandleAchievementGetResponse(IRestResponse response)
        {
            JsonDeserializer deserializer = new JsonDeserializer();
            AchievementGamecloud result = deserializer.Deserialize<AchievementGamecloud>(response);
        }

        /// <summary>
        /// Handles the Get Event Response from the Gamecloud
        /// </summary>
        /// <param name="response">The response received from the Gamecloud</param>
        public void HandleEventGetResponse(IRestResponse response)
        {
            JsonDeserializer deserializer = new JsonDeserializer();
            EventGamecloud result = deserializer.Deserialize<EventGamecloud>(response);
        }
        
        
    } // End of GamecloudCallbacks class


} // End of namespace Gamecloud
