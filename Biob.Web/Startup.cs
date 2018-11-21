﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Biob.Data.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using AutoMapper;
using Biob.Data.Models;
using Biob.Services.Data.DtoModels;
using Biob.Web.Helpers;
using Biob.Services.Data.Repositories;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Biob.Services.Web.PropertyMapping;
using Microsoft.AspNetCore.Mvc.Formatters;
using Newtonsoft.Json.Serialization;

namespace Biob.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(setupAction =>
            {
                setupAction.ReturnHttpNotAcceptable = true;
                setupAction.OutputFormatters.Add(new XmlDataContractSerializerOutputFormatter());

                setupAction.InputFormatters.Add(new XmlSerializerInputFormatter(setupAction));


            })
            .AddJsonOptions(options =>
            {
                options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            var connectionString = Configuration.GetConnectionString("BiobDB");
            services.AddDbContext<BiobDataContext>(options => options.UseSqlServer(connectionString));
            services.AddScoped<IMovieRepository, MovieRepository>();


            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();

            services.AddScoped<IUrlHelper, UrlHelper>(implementationFactory =>
            {
                var actionContext = implementationFactory.GetService<IActionContextAccessor>().ActionContext;
                return new UrlHelper(actionContext);
            });

            services.AddTransient<IPropertyMappingService, PropertyMappingSerice>();
            services.AddTransient<ITypeHelperService, TypeHelperService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            Mapper.Initialize(config => 
            {
                config.CreateMap<Movie, MovieDto>()
                .ForMember(dest => dest.Length, opt => opt.MapFrom(src => src.LengthInSeconds.CalculateFromSeconds()));
                config.CreateMap<MovieToCreateDto, Movie>();
                config.CreateMap<MovieToUpdateDto, Movie>();
                config.CreateMap<Movie, MovieToUpdateDto>();
            });

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
