//using System.Reflection;

//namespace Application
//{
//    public static class ServiceExtentions
//    {
//        public static void AddApplication(this IServiceCollection services)
//        {
//            services.AddAutoMapper(Assembly.GetExecutingAssembly());

//            // registration of mediatr
//            services.AddMediatR(conf => conf.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

//            // registration of fluent validator
//            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

//            // registration of pipeline behavior
//            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviors<,>));
//        }
//    }
//}
