angular
	.module('CanadaAlertSystem', ['ngRoute', 'pascalprecht.translate'])
	
	.filter('fdate', function($filter, $translate) {
		return function(date) {
			return $filter('date')(date, $translate('DATE_FORMAT'));
		}
	})
	
	// TRASLATIONS
	// ===========
	// Provides translations for English and French
	.config(function ($translateProvider) {
		$translateProvider
			.translations('en-CA', {
				APPLICATION_TITLE : 'Canada Alert System',
				
				LANGUAGE_TEXT : 'Language',
				
				ISSUED_BY_TEXT : 'Issued by',
				ISSUED_ON_TEXT : 'on',
				EXPIRES_TEXT : 'Expires on',
				AREAS_FOR_TEXT : 'For',
				
				DATE_FORMAT : "dd-MM-yyyy 'at' h:mma"
			})
			.translations('fr-CA', {
				APPLICATION_TITLE : 'Système Alerte du Canada',
				
				LANGUAGE_TEXT : 'Langue',
				
				ISSUED_BY_TEXT : 'Émis par',
				ISSUED_ON_TEXT : 'le',
				EXPIRES_TEXT : 'Expire le',
				AREAS_FOR_TEXT : 'Pour',
				
				DATE_FORMAT : "dd-MM-yyyy 'à' H:mm"
			});
		
		$translateProvider.preferredLanguage('en-CA');
	})
	
	.controller('LanguageSelection', function($scope, $rootScope, $translate) {
		$scope.language = 'en-CA';
		$scope.$watch('language', function () {
			$rootScope.language = $scope.language;
			$translate.uses($scope.language);
		});
	})
	
	// ALERTS
	// ======
	// Alerts controller.
	.controller('Alerts', function($scope, $rootScope, $http, $translate) {
		$scope.language = $translate.uses();
		
		$rootScope.$watch('language', function() {
			$scope.language = $translate.uses();
		});
		
		$scope.alerts = [];
		$http.get('http://10.0.1.5/api/alerts')
				.success(function(alerts) {
					$scope.alerts = alerts;
				})
				.error(function(error) {
					console.log(error);
					alert("An error occurred loading alerts.");
				});
	
	});