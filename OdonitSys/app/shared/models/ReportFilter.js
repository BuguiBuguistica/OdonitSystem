app.factory('ReportFilterModel', function () {
    return {
        Filter: function () {
            this.ServiceName='';
            this.ServiceId=null;
            this.TreatmentName = '';
            this.TreatmentId = null;
            this.MedicalSecurityName = '';
            this.MedicalSecurityId = null;
            this.FromDate = null;
            this.EndDate = null;
        }
    }
});