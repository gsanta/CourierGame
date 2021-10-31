namespace GameObjects
{
    public interface IGameObjectSelector
    {
        void Select();
        void Deselect();

        void SetGameObject(ISelectableGameObject selectableGameObject);
    }
}
