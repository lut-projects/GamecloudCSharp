using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RestSharp;

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

        public void HandleResponse(IRestResponse response)
        {
            // And write the response
            Console.WriteLine(response.Content);

        }
    }
}
