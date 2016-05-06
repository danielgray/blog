(function () {

    angular.module("app-story")
        .controller("storyController", ['$scope', '$http', '$location', function($scope, $http, $location) {

            // get stroy id from query string
            var splits = $location.absUrl().split("/");
            var id = splits[splits.length - 1];

            var vm = this;
            // set up a new sotry
            vm.newStory = {};
            vm.newStory.title = "Title";
            vm.newStory.content = "Content";

            // try find an existing story
            $http.get("/api/stories").success(function (response) {
                vm.story = $.grep(response, function (e) { return e.id == id; })[0];
            });

            vm.addStory = function () {
                $http.post("/api/stories", vm.newStory)
                    .success(function (data, status) {
                        // create a redirect to the edit of this story
                        window.location.href = window.location.origin + "/Blog/EditStory/" + data.data.id;
                    })
            };

            vm.updateStory = function () {
                $http.put("/api/stories", vm.story)
                    .success(function (data, status) {
                        // do popup
                        vm.story = data;
                    })
            };

            vm.deleteStory = function () {
                $http.delete("/api/stories", vm.newStory)
                    .success(function (data, status) {
                        // TODO: add some handling code
                    })
            };

        }]);

})();