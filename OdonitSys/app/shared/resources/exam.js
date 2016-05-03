app.factory('ExamResource', ['globals', '$resource', function (globals, $resource) {
    return $resource(globals.webApi + '/Exam/v1/:param', {}, {});
}])