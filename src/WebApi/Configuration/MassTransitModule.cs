namespace WebApi.Configuration
{
    using System;
    using Autofac;
    using Consumers;
    using Filters;
    using GreenPipes;
    using MassTransit;


    public class MassTransitModule : Module
    {
        private static readonly string ScopeTag = "mt-message";

        /// <inheritdoc />
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.AddMassTransit(x =>
            {
                x.AddConsumer<AddInventoryConsumer>();

                x.UsingInMemory((context, cfg) =>
                {
                    AutofacFilterExtensions.UseSendFilter(cfg, typeof(TokenSendFilter<>), context);
                    AutofacFilterExtensions.UsePublishFilter(cfg, typeof(TokenPublishFilter<>), context);
                    AutofacFilterExtensions.UseConsumeFilter(cfg, typeof(TokenConsumeFilter<>), context);

                    cfg.UseMessageRetry(retry => { retry.Interval(1, TimeSpan.FromSeconds(1)); });

                    cfg.UseMessageLifetimeScope(context.GetRequiredService<ILifetimeScope>(), ScopeTag,
                        (containerBuilder, consumeContext) =>
                        {
                            // other registrations
                        }
                    );
                    cfg.UseInMemoryOutbox();
                    cfg.ConfigureEndpoints(context);
                });
            });

            builder.RegisterType<Token>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<TokenActionFilter>().AsSelf().InstancePerLifetimeScope();
        }
    }
}
