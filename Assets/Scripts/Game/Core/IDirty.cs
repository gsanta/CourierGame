
namespace Core
{
    public interface IDirty
    {
        public bool IsDirty();
        public void ClearDirty();
    }
}
