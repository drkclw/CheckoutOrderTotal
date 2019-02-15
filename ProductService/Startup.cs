using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ProductService.Models;
using ProductService.Models.Prices;
using ProductService.Models.Specials;
using ProductService.Models.Markdowns;

namespace ProductService
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddSingleton<IRepository<Product>, PriceRepository>();
            services.AddSingleton<IRepository<ISpecial>, SpecialsRepository>();
            services.AddSingleton<IRepository<Markdown>, MarkdownRepository>();
            services.AddTransient<IDataAccessor<Markdown>, MarkdownDataAccessor>();
            services.AddTransient<IDataAccessor<Product>, PriceDataAccessor>();
            services.AddTransient<IDataAccessor<ISpecial>, SpecialsDataAccessor>();
            services.AddTransient<IValidator<ISpecial>, SpecialsValidator>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
