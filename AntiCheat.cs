using UnityEngine;
using MelonLoader;
using Il2CppDecaGames.RotMG.Managers;
using Il2CppCodeStage.AntiCheat.Detectors; // this is AntiCheat using , also known as the dependency: "Il2CppACTk.Runtime"

namespace AntiCheatRemover
{

    // made by mimi
    // Credit to RotMG AntiCheat discord: https://discord.gg/wcJ9S5z7Mx 
    public class AntiCheat : MelonMod
    {   
        private bool isMain = false; // looking for our main scene, determines when the game is loaded




        private void RemoveComponent<T>(Transform parent) where T : Component
        {
            T component = parent.GetComponent<T>();
            if (component != null)
            {
                UnityEngine.Object.Destroy(component); // technically we are making a func to destory the component which we then call below (RemoveComponent)
            }
        }

        private void AntiCheatToolkit() // our func (obviously)
        {
            GameObject gameController = GameObject.Find("GameController"); // goes through the generic game object until it finds GameController, this is where speedhack detector and anti-cheat toolkit lie (inside gamecontroller)
            if (gameController != null)
            {
                RemoveComponent<AntiCheatManager>(gameController.transform); // this is where we remove the anticheatmanager component (1st thing we remove)

                Transform antiCheatToolkit = gameController.transform.Find("Anti-Cheat Toolkit"); // finding component anti-cheat toolkit 
                if (antiCheatToolkit != null)
                {
                    RemoveComponent<SpeedHackDetector>(antiCheatToolkit); // removing component speedhack detector (2nd thing we remove)
                }
            }
        }

        public override void OnSceneWasLoaded(int buildIndex, string sceneName) // here we use to call any scenes we want, in this case we are using Main
        {
            isMain = sceneName == "Main";
            if (isMain)
            {
                AntiCheatToolkit(); // calling the AntiCheatToolkit function, which removes the anticheat as our main scene is loaded
                MelonLogger.Log("AntiCheat removed");
            }
        }
    }
}