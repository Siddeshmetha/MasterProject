using MasterProjectBAL.Product;
using MasterProjectBAL.Relation;
using MasterProjectBAL.School;
using MasterProjectBAL.User;
using MasterProjectBAL.UserMember;
using MasterProjectCommonUtility.Configuration;
using MasterProjectCommonUtility.Logger;
using MasterProjectCommonUtility.Paging;
using MasterProjectDAL.DataModel;
using MasterProjectDAL.EmailRepo;
using MasterProjectDAL.Product;
using MasterProjectDAL.RelationRepo;
using MasterProjectDAL.SchoolRepo;
using MasterProjectDAL.UserMemberRepo;
using MasterProjectDAL.UserRepo;
using Microsoft.EntityFrameworkCore;

namespace MasterProjectWebAPI.Helper
{
    public class ServiceRegistry
    {
        public void ConfigureDependencies(IServiceCollection services, AppsettingsConfig appSettings)
        {
            #region Bussiness Layer
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserMemberService, UserMemberService>();
            services.AddScoped<IUserMemberService, UserMemberService>();
            services.AddScoped<ISchoolService, SchoolService>();
            services.AddScoped<IRelationService, RelationService>();
            #endregion

            #region Data Layer
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IEmailRepository, EmailRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserMemberRepository, UserMemberRepository>();
            services.AddScoped<ISchoolRepo, SchoolRepo>();
            services.AddScoped<IRelationRepo, RelationRepo>();
            #endregion

            #region Common Layer
            services.AddSingleton<ILoggerManager, LoggerManager>();
            services.AddSingleton<IPagingParameter, PagingParameter>();
            #endregion
        }
        public void ConfigureDataContext(IServiceCollection services, AppsettingsConfig appSettings)
        {
            //Added LogFactory
            var loggerFactory = LoggerFactory.Create(builder =>
            {
                builder
                    .AddConsole()
                    .AddFilter(DbLoggerCategory.Database.Command.Name, LogLevel.Information);
            });
            var connString = appSettings.MasterProjectData.ConnectToDb.ConnectionString;
            services.AddDbContext<IMasterProjectContext, MasterProjectContext>(options =>
            {
                options.UseMySql(connString, new MySqlServerVersion(new Version(8, 0, 37))).EnableSensitiveDataLogging().UseLoggerFactory(loggerFactory);
            });
        }
    }
}
