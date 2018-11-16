using AutoMapper;



namespace Менеждер_заказов.Service
{
    public static class MyMapper
    {
        private static bool _isInitialized;
        public static void Initialize()
        {
            if (!_isInitialized)
            {
                Mapper.Initialize(cfg =>
                {
                    //cfg.CreateMap<ModelDB.Project, ModelDB.Project>();
                    //cfg.CreateMap<ModelDB.Order, ModelDB.Order>();

                });
                _isInitialized = true;
            }
        }
    }
}
