var design_module = angular.module('design', ['ngResource', 'ui.router', 'ui.bootstrap']);

//Config
design_module.config(DesignRoute);

// Service
design_module.service('MetaService', ['$resource', '$q', MetaService]);
design_module.service('DBService', ['$resource', '$q', DBService]);

// Directive
design_module.directive('field', FieldDirective);

// Controller
design_module.controller(WelcomeController);
design_module.controller(DBsController);
design_module.controller(DBController);
design_module.controller(PreviewController);

//modal
design_module.controller(CreateDBController);
design_module.controller(CreateTableController);
