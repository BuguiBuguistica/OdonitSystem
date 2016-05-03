app.factory('MedicalHistoryModel', ['ExamModel', function (ExamModel) {
    return {
        MedicalHistory: function () {
            CurrentMedication = '';
            BloodGroup = '';
            Alergic = false;
            Hemorrhage = false;
            Diabetes = false;
            HeartDisease = false;
            RespiratoryDisease = false;
            KidneyDisease = false;
            Hepatitis = false;
            Hypertension = false;
            STD = false;
            PregnancyDate = null;
            TransfusionDate = null;
            Observations = '';
            Habits = '';
            Others = '';
            Exam = new ExamModel.Exam();
        }
    }
}]);