using AI;
using GamePlay;
using System.Collections.Generic;

namespace GameObjects
{
    public class EnterBuildingPostAction : IGPostAction
    {
        private SceneManager sceneManager;
        private SubsceneStore subsceneStore;
        private CharacterStore characterStore;

        public EnterBuildingPostAction(SceneManager sceneManager, SubsceneStore subsceneStore, CharacterStore characterStore)
        {
            this.sceneManager = sceneManager;
            this.subsceneStore = subsceneStore;
            this.characterStore = characterStore;
        }

        public IGPostAction Clone()
        {
            return new EnterBuildingPostAction(sceneManager, subsceneStore, characterStore);
        }

        public void Execute()
        {
            subsceneStore.Type = SubsceneType.BUILDING;
            subsceneStore.SubSceneId = "Building";
            subsceneStore.AddCharacter(characterStore.GetActivePlayer());
            sceneManager.EnterSubScene();
        }
    }
}
