(function () {

    angular.module("app-story")
        .controller("adminController", ['$scope', '$http', function($scope, $http) {

            var vm = this;

            vm.get = function () {

                $http.get("/api/stories").success(function (response) {
                    vm.stories = response;
                });

            }

            vm.stories = {};
            vm.get();

            vm.addStory = function () {
                $http.post("/api/stories", vm.newStory)
                    .success(function (data, message) {
                        debugger

                        if (message === "Created") {
                            vm.newStory = data;
                        }
                    })
            };

            vm.editStory = function (story) {



            }

            vm.deleteStory = function (story) {

                $http.delete("/api/stories/" + story.id )
                    .success(function (data) {

                        if (data === "") {
                            vm.get();
                        }

                    })
            };

        }]);

})();