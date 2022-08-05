﻿using Marajoara.Cinema.Management.Domain.CineRoomModule;
using Marajoara.Cinema.Management.Domain.UnitOfWork;
using Marajoara.Cinema.Management.Infra.Data.EF;
using Marajoara.Cinema.Management.Infra.Data.EF.Commom;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Respawn;
using System.Data.Entity;
using System.Data.SqlClient;


namespace Marajoara.Cinema.Management.Tests.Common
{
    [TestClass]
    public class TestsIntegrationBase
    {
        private static string _connectionString;
        protected static IMarajoaraUnitOfWork _marajoaraUnitOfWork;

        public static IMarajoaraUnitOfWork GetNewEmptyUnitOfWorkInstance()
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.InitialCatalog = "MarajoaraTestIntegration";
            builder.DataSource = "(localdb)\\mssqllocaldb";
            _connectionString = builder.ConnectionString;
            MarajoaraContext context = new MarajoaraContext(new SqlConnection(_connectionString));
            try
            {
                //context.Database.ForceDelete();
            }
            catch (SqlException) { /*Could not delete, because database was not createad*/ }

            Database.SetInitializer(new DropCreateDatabaseAlways<MarajoaraContext>());
            context.Database.Initialize(true);

            ICineRoomRepository cineRoomRepository = new CineRoomRepository(context);

            IMarajoaraUnitOfWork unitOfWork = new MarajoaraUnitOfWork(context, cineRoomRepository);

            return unitOfWork;
        }
    }
}
