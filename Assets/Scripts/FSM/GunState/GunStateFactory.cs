public class GunStateFactory 
{
    GunStateManager _contex;

    public GunStateFactory(GunStateManager currentContex)
    {
        _contex = currentContex;
    }

    public GunBaseState Ready()
    {
        return new GunFreeState(_contex, this);
    }

    public GunBaseState Fire()
    {
        return new GunShottingState(_contex, this);
    }

    public GunBaseState Reload()
    {
        return new GunReloadState(_contex, this);
    }

}
