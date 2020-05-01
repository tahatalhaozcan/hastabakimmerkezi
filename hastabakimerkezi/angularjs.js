var app = angular.module("myApp", ["ngRoute"]);
app.config(function ($routeProvider) {
    $routeProvider
        .when("/giris", {
            templateUrl: "giris.html",
            controller: "girisCtrl"
        })
        .when("/yonetim", {
            templateUrl: "yonetim.html",
            controller: "yonetimCtrl"
        })
        .when("/giriscikis", {
            templateUrl: "hastagiriscikis.html",
            controller: "gcCtrl"
        })
        .when("/islem", {
            templateUrl: "hastaislem.html",
            controller: "islemCtrl"

        });

});
app.controller('islemCtrl', function ($scope, $http) {
    $http.get("http://localhost:59387/api/Hasta/HastalariGetir")
        .then(function (response) {
            $scope.hastalarigetir = response.data;
        });
    $http.get("http://localhost:59387/api/Ilac/IlaclariGetir")
        .then(function (response) {
            $scope.ilaclarigetir = response.data;
        });
    $http.get("http://localhost:59387/api/Doktor/DoktorlariGetir")
        .then(function (response) {
            $scope.doktorlarigetir = response.data;
        });

    $scope.bilgilerigetir = function (hid) {
        $http.get("http://localhost:59387/api/Bakim/BakilanHastalar?hastaID=" + hid)
            .then(function (response) {
                $scope.hastabilgileri = response.data;
            });
    }
    $scope.tedavibitir = function (tb) {
        alert("Tedavi tamamlanıyor.");
        $http.get("http://localhost:59387/api/Bakim/KlinikCikis?bakimID=" +tb)
            .then(function (response) {
                $scope.hastabilgileri = response.data;
            });
    }
    $scope.tedaviolustur = function (hid, did, iid) {
        $scope.veri =
            {
                hastaID: hid,
                doktorID: did,
                ilacID: iid
                
            }
        $http.post("http://localhost:59387/api/Bakim/KlinikYeniGiris", $scope.veri)
            .then(function (response) {
                $scope.hastabilgileri = response.data;
            });
        alert("Tedavi Başlatılıyor")


    }
});
app.controller('gcCtrl', function ($scope, $http) {
    $http.get("http://localhost:59387/api/Hasta/HastalariGetir")
        .then(function (response) {
            $scope.hastalarigetir = response.data;
        });
    $scope.taburcu = function (tid) {
        alert("Taburcu işlemi tamamlanıyor");
        $http.get("http://localhost:59387/api/Hasta/HastaSil?hastaID=" + tid)
            .then(function (response) {
            $scope.hastalarigetir = response.data;
            });
    }
    $scope.hastaekle = function (kayit)
    {
        $http.post("http://localhost:59387/api/Hasta/YeniHasta", kayit)
            .then(function (response) {
                $scope.hastalarigetir = response.data;
                $scope.kullanici = {};
                
            });
    }
    
});
app.controller('girisCtrl', function ($scope, $http, $window) {
    $scope.ygirisyap = function (ka, sifre) {
        $http.get("http://localhost:59387/api/Giris/AdminGiris?ka=" + ka + "&sifre=" + sifre)
            .then(function (response) {
                if (response.data != 0) {
                    $window.location.href = '/yonetim.html'
                }
                else {
                    alert("Hatalı giriş denemesi");
                }
            });
    }
    $scope.pgirisyap = function (kap, sifre) {
        $http.get("http://localhost:59387/api/Giris/PersonelGiris?kap=" + kap + "&sifre=" + sifre)
            .then(function (response) {
                if (response.data != 0) {
                    $window.location.href = '/hastagiriscikis.html'
                }
                else {
                    alert("Hatalı giriş denemesi");
                }
            });
    }
});

app.controller('yonetimCtrl', function ($scope, $http) {
    $http.get("http://localhost:59387/api/Hasta/HastalariGetir")
        .then(function (response) {
            $scope.hastalarigetir = response.data;
        });
    $http.get("http://localhost:59387/api/Ilac/IlaclariGetir")
        .then(function (response) {
            $scope.ilaclarigetir = response.data;
        });
    $http.get("http://localhost:59387/api/Doktor/DoktorlariGetir")
        .then(function (response) {
            $scope.doktorlarigetir = response.data;
        });
    $scope.isinesonver = function (tid) {
        alert("İş akdi feshediliyor");
        $http.get("http://localhost:59387/api/Doktor/DoktorSil?doktorID=" + tid)
            .then(function (response) {
                $scope.doktorlarigetir = response.data;
            });
    }
    $scope.ilacsil = function (tid) {
        alert("İlaç tedarik edilmeyecek");
        $http.get("http://localhost:59387/api/Ilac/IlacSil?ilacID=" + tid)
            .then(function (response) {
                $scope.ilaclarigetir = response.data;
            });
    }
    $scope.ilacekle = function (giris) {
        $http.post("http://localhost:59387/api/Ilac/YeniIlac", giris)
            .then(function (response) {
                $scope.ilaclarigetir = response.data;
                $scope.kullanici = {};

            });
    }


});

  