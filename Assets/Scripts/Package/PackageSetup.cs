
public class PackageSetup
{
    private PackageStore packageStore;
    private PackageFactory packageFactory;
    private PackageSpawner packageSpawner;

    public PackageSetup(PackageStore packageStore, PackageFactory packageFactory, PackageSpawner packageSpawner)
    {
        this.packageStore = packageStore;
        this.packageFactory = packageFactory;
        this.packageSpawner = packageSpawner;
    }

    public void Setup()
    {
        // TODO this should be done by some package creation strategy
        Package package = packageFactory.CreatePackage(packageSpawner.Spawn());
        packageStore.Add(package);
    }
}
