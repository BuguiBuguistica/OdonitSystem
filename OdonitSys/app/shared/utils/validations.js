app.factory('validations', function () {
    return {
        isEmpty: function (value) {
            if (value === "" || value === null || value === undefined) {
                return true;
            } else {
                return false;
            };
        },
        isNumeric: function (value) {
            if (!value) { return;};
            if (value.match(/^[0-9]+$/)) {
                return true;
            } else {
                return false;
            }
            
        },
        isEmail: function (value) {
            if (!value) { return; }
            if (value.match(/^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,4})+$/)) {
                return true;
            } else {
                return false;
            }

        }
    }
});