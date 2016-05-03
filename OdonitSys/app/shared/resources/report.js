app.factory('ReportResource', ['globals', '$resource', function (globals, $resource) {
    return $resource(globals.webApi + '/PatientsReport/v1', {}, {
        get:{method:'GET', isArray:true},
        report: {
            method: 'POST',
            responseType: 'arraybuffer',
            transformResponse: function (data) {
                var pdf;
                if (data) {
                    pdf = new Blob([data], {
                        type: 'application/pdf'
                    });
                }
                return {
                    response: pdf
                };
            }
        },
    });
}])