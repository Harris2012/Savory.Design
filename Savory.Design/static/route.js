var DesignRoute = function ($stateProvider, $urlRouterProvider) {
    $urlRouterProvider.otherwise('/404');

    $stateProvider
        .state('index', {
            url: '',
            templateUrl: '/static/controller/WelcomeView.html',
            controller: WelcomeController
        })
        .state('welcome', {
            url: '/',
            templateUrl: '/static/controller/WelcomeView.html',
            controller: WelcomeController
        })
        .state('dbs', {
            url: '/dbs',
            templateUrl: '/static/controller/DBsView.html',
            controller: DBsController
        })
        .state('db', {
            url: '/db/:dbName',
            templateUrl: '/static/controller/DBView.html',
            controller: DBController
        })
        .state('preview', {
            url: '/preview',
            templateUrl: '/static/controller/PreviewView.html',
            controller: PreviewController
        })
        .state('404', {
            url: '/404',
            templateUrl: '/static/include/404View.html'
        });

    //.when('/promotion/:promotionId', {
    //    templateUrl: 'html/promotion.html',
    //    controller: 'promotion_controller'
    //})
}