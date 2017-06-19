(function () {
    "use strict";

    // Create the module and add dependency modules.
    // Defines a constant for module.
    angular
        .module("common.services",
                    ["ngResource"])
        .constant("appSettings",
        {
            serverPath: "http://localhost:53654/"
        });
})();