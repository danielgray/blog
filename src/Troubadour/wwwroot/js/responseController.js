(function () {

    angular.module("app-response")
        .controller("responseController", ['$scope', '$http', '$location', function ($scope, $http, $location) {

            var splits = $location.absUrl().split("/");
            var id = splits[splits.length - 1];

            debugger

            var vm = this;
            vm.content = "";

            // try find an existing story
            $http.get("/api/stories").success(function (response) {
                vm.story = $.grep(response, function (e) { return e.id == id; })[0];
            });


            vm.addResponse = function () {

                var data = { 'story': vm.story, 'content': vm.content.toString() };

                debugger

                $http.post("/api/responses", data)
                    .success(function (data, status) {
                        vm.newStory = data;
                    })
            };

            vm.updateStory = function () {
                $http.put("/api/stories", vm.story)
                    .success(function (data, status) {
                        vm.story = data;
                    })
            };

            vm.deleteStory = function () {
                $http.delete("/api/stories", vm.newStory)
                    .success(function (data, status) {

                        debugger
                    })
            };

        }]);

})();