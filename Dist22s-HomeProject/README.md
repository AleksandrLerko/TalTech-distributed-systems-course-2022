# Dits22s Project Online Shopping System

### Link to the website https://allerk-dist22-frontend.azurewebsites.net/

## EF tools
~~~sh
dotnet tool install -g dotnet-ef
dotnet tool update -g dotnet-ef
dotnet tool install --global dotnet-aspnet-codegenerator
dotnet tool update --global dotnet-aspnet-codegenerator
~~~

## Scaffolding

### Database
~~~sh
dotnet ef migrations add --project App.DAL.EF --startup-project WebApp --context AppDbContext Initial
dotnet ef database update --project App.DAL.EF --startup-project WebApp --context AppDbContext
dotnet ef database remove --project App.DAL.EF --startup-project WebApp
dotnet ef database drop --project App.DAL.EF --startup-project WebApp
~~~

### Web Controllers
~~~sh
cd WebApp
dotnet aspnet-codegenerator controller -name CategoriesController -actions -m Domain.Category -dc AppDbContext -outDir Areas\Admin\Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name CurrenciesController -actions -m Domain.Currency -dc AppDbContext -outDir Areas\Admin\Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name CustomersController -actions -m Domain.Customer -dc AppDbContext -outDir Areas\Admin\Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name DeliveryTypesController -actions -m Domain.DeliveryType -dc AppDbContext -outDir Areas\Admin\Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name FeedbacksController -actions -m Domain.Feedback -dc AppDbContext -outDir Areas\Admin\Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name InStocksController -actions -m Domain.InStock -dc AppDbContext -outDir Areas\Admin\Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name LocationsController -actions -m Domain.Location -dc AppDbContext -outDir Areas\Admin\Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name OrdersController -actions -m Domain.Order -dc AppDbContext -outDir Areas\Admin\Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name PaymentTypesController -actions -m Domain.PaymentType -dc AppDbContext -outDir Areas\Admin\Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name ProductsController -actions -m Domain.Product -dc AppDbContext -outDir Areas\Admin\Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name SellersController -actions -m Domain.Seller -dc AppDbContext -outDir Areas\Admin\Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name ShippingInfosController -actions -m Domain.ShippingInfo -dc AppDbContext -outDir Areas\Admin\Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name ShippingInfoAppUsersController -actions -m Domain.ShippingInfoAppUser -dc AppDbContext -outDir Areas\Admin\Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name ShippingInfoCustomersController -actions -m Domain.ShippingInfoCustomer -dc AppDbContext -outDir Areas\Admin\Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name SpecificationsController -actions -m Domain.Specification -dc AppDbContext -outDir Areas\Admin\Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name SpecificationTypesController -actions -m Domain.SpecificationType -dc AppDbContext -outDir Areas\Admin\Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name TransactionReportsController -actions -m Domain.TransactionReport -dc AppDbContext -outDir Areas\Admin\Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name CategoryTypesController -actions -m Domain.CategoryType -dc AppDbContext -outDir Areas\Admin\Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
~~~

### Web Api Controllers
~~~sh
cd WebApp
dotnet aspnet-codegenerator controller -name ProductsController -actions -m Domain.Product -dc AppDbContext -outDir ApiControllers --useDefaultLayout -api --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name CategoriesController -actions -m Domain.Category -dc AppDbContext -outDir ApiControllers --useDefaultLayout -api --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name CurrenciesController -actions -m Domain.Currency  -dc AppDbContext -outDir ApiControllers --useDefaultLayout -api --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name SellersController -actions -m Domain.Seller -dc AppDbContext -outDir ApiControllers --useDefaultLayout -api --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name ShippingInfosController -actions -m Domain.ShippingInfo -dc AppDbContext -outDir ApiControllers --useDefaultLayout -api --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name ShippingInfoAppUsersController -actions -m Domain.ShippingInfoAppUser -dc AppDbContext -outDir ApiControllers --useDefaultLayout -api --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name ShippingInfoCustomersController -actions -m Domain.ShippingInfoCustomer -dc AppDbContext -outDir ApiControllers --useDefaultLayout -api --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name SpecificationsController -actions -m Domain.Specification -dc AppDbContext -outDir ApiControllers --useDefaultLayout -api --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name SpecificationTypesController -actions -m Domain.SpecificationType -dc AppDbContext -outDir ApiControllers --useDefaultLayout -api --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name TransactionReportsController -actions -m Domain.TransactionReport -dc AppDbContext -outDir ApiControllers --useDefaultLayout -api --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name CategoryTypesController -actions -m Domain.CategoryType -dc AppDbContext -outDir ApiControllers --useDefaultLayout -api --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name CustomersController -actions -m Domain.Customer -dc AppDbContext -outDir ApiControllers --useDefaultLayout -api --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name DeliveryTypesController -actions -m Domain.DeliveryType -dc AppDbContext -outDir ApiControllers --useDefaultLayout -api --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name FeedbacksController -actions -m Domain.Feedback -dc AppDbContext -outDir ApiControllers --useDefaultLayout -api --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name InStocksController -actions -m Domain.InStock -dc AppDbContext -outDir ApiControllers --useDefaultLayout -api --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name LocationsController -actions -m Domain.Location -dc AppDbContext -outDir ApiControllers --useDefaultLayout -api --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name OrdersController -actions -m Domain.Order -dc AppDbContext -outDir ApiControllers --useDefaultLayout -api --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name PaymentTypesController -actions -m Domain.PaymentType -dc AppDbContext -outDir ApiControllers --useDefaultLayout -api --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name PicturesController -actions -m App.Domain.Picture -dc AppDbContext -outDir ApiControllers --useDefaultLayout -api --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name ProductOrdersController -actions -m App.Domain.ProductOrders -dc AppDbContext -outDir ApiControllers --useDefaultLayout -api --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name InvoicesController -actions -m App.Domain.Invoice -dc AppDbContext -outDir ApiControllers --useDefaultLayout -api --useAsyncActions --referenceScriptLibraries -f
~~~

