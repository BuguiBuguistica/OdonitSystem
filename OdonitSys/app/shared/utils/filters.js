app.filter('translateBoolean', function () {
    return function (input) {
        return input ? 'SI' : 'NO';
    };
});