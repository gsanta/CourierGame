
public class PackageSetup
{
    private PackageStore packageStore;
    private PackageFactory packageFactory;

    public PackageSetup(PackageStore packageStore, PackageFactory packageFactory)
    {
        this.packageStore = packageStore;
        this.packageFactory = packageFactory;
    }

    public void Setup()
    {
        // TODO this should be done by some package creation strategy
        //Package package = packageFactory.CreatePackage();
        //packageStore.Add(package);
    }
}
