app.factory('PersonModel', ['MedicalHistoryModel', 'ContactModel', 'AddressModel', function (MedicalHistoryModel, ContactModel, AddressModel) {
    return {
        Person: function (birthday) {
            this.FirstName = '';
            this.LastName = '';
            this.Age = null;
            this.BirthDate = '';
            this.Gender = 0;
            this.Code = '';
            this.Address = new AddressModel.Address();
            this.Contact = new ContactModel.Contact();
            this.IsActive = true;
            this.MedicalSecurityId = null;
            this.SecurityPlan = '';
            this.SecurityNumber = null;
            this.MedicalHistory = new MedicalHistoryModel.MedicalHistory();
        }
    }
}])
