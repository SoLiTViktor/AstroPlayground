using Firebase.Extensions;
using Firebase;
using UnityEngine;
using System.Runtime.CompilerServices;

namespace AsteroidProject
{
    public class FirebaseSetup
    {
        public FirebaseSetup()
        {
            FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
                    {
                        var dependencyStatus = task.Result;
                        if (dependencyStatus == DependencyStatus.Available)
                        {
                            FirebaseApp app = FirebaseApp.DefaultInstance;
                        }
                        else
                        {
                            Debug.LogError(System.String.Format(
                            "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
                        }
                    });
        }
    }
}