//angular.module('MyApp')  //extending angular module from first part
//.controller('PartController', function ($scope, DiagnosisService) { //explained about controller in Part2
//    $scope.Diagnosis = [];

//    DiagnosisService.GetDiagnosis().then(function (d) {
//        $scope.Diagnosis = d.data;
//    });
//})
//.factory('DiagnosisService', function ($http) { //explained about factory in Part2
//    var fac = {};
//    fac.GetDiagnosis = function () {
//        return $http.get('/Pasien/PasienDetails');
//    }
//    return fac;
//});