using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        // Lista e doktorëve me numrat e telefonit
        Dictionary<string, string> doctors = new Dictionary<string, string>
        {
            { "Dr. Besnik Kelmendi", "+38349400984" },
            { "Dr. Arta Krasniqi", "+38349302740" },
            { "Dr. Mentor Sadiku", "+38344510848" },
            { "Dr. Valon Dervishi", "+38349111111" },
            { "Dr. Ilir Kastrati", "+38344121212" }
        };

        // Lista statike e pacientëve
        List<Patient> patients = new List<Patient>
        {
            new Patient("Arben Hoxha", 101, "Dr. Besnik Kelmendi"),
            new Patient("Elira Berisha", 102, "Dr. Arta Krasniqi"),
            new Patient("Luan Gashi", 101, "Dr. Mentor Sadiku"),
            new Patient("Blerim Krasniqi", 104, "Dr. Valon Dervishi"),
            new Patient("Alma Sadiku", 102, "Dr. Ilir Kastrati"),
            new Patient("Driton Shala", 104, "Dr. Besnik Kelmendi"),
            new Patient("Gentiana Zeka", 103, "Dr. Arta Krasniqi"),
            new Patient("Florent Rexhepi", 103, "Dr. Mentor Sadiku"),
            new Patient("Arian Salihu", 103, "Dr. Valon Dervishi"),
            new Patient("Leonora Thaçi", 104, "Dr. Ilir Kastrati"),
            new Patient("Shpend Ramadani", 102, "Dr. Besnik Kelmendi"),
            new Patient("Fatmire Leka", 105, "Dr. Arta Krasniqi"),
            new Patient("Visar Hoti", 104, "Dr. Mentor Sadiku"),
            new Patient("Rina Begu", 103, "Dr. Valon Dervishi"),
            new Patient("Granit Morina", 102, "Dr. Ilir Kastrati")
        };

        // Simulimi i matjeve për secilin pacient
        foreach (var patient in patients)
        {
            patient.MeasureBloodPressure();
            Console.WriteLine(patient);

            // Dërgo SMS bazuar në klasifikimin e tensionit të gjakut
            string message = null;

            if (patient.Systolic >= 180 || patient.Diastolic >= 110)
                message = $"Hypertensive Emergency! {patient.Name} ka tension shumë të lartë!";
            else if (patient.Systolic >= 160 || patient.Diastolic >= 100)
                message = $"Moderate Hypertension! {patient.Name} ka nevojë për vlerësim mjekësor.";
            else if (patient.Systolic >= 140 || patient.Diastolic >= 90)
                message = $"Mild Hypertension! {patient.Name} ka tension të lartë, këshillohet kujdes.";
            else if (patient.Systolic >= 130 || patient.Diastolic >= 85)
                message = $"High Normal Blood Pressure! {patient.Name} duhet të monitorohet.";
            else if (patient.Systolic < 130 && patient.Diastolic < 85)
                message = $"Normal Blood Pressure! {patient.Name} ka presion të ulët.";

            if (message != null)
            {
                if (doctors.TryGetValue(patient.Doctor, out string doctorPhone))
                {
                    if(patient.Systolic > 160 && patient.Diastolic > 100)
                        SendSMS(patient.Doctor, doctorPhone, message);
                }
                else
                {
                    Console.WriteLine($"[Gabim] Nuk u gjet numri i telefonit për {patient.Doctor}");
                }
            }
        }
    }

    static void SendSMS(string doctorName, string doctorPhone, string message)
    {
        Console.WriteLine($"[SMS] Dërgohet tek {doctorName} ({doctorPhone}): {message}\n");
    }
}

class Patient
{
    public string Name { get; }
    public int RoomNumber { get; }
    public string Doctor { get; }
    public int Systolic { get; private set; }
    public int Diastolic { get; private set; }

    private static Random random = new Random();

    public Patient(string name, int roomNumber, string doctor)
    {
        Name = name;
        RoomNumber = roomNumber;
        Doctor = doctor;
    }

    public void MeasureBloodPressure()
    {
        Systolic = random.Next(85, 190);  // Vlera e rastësishme për systolic (85-190)
        Diastolic = random.Next(55, 115); // Vlera e rastësishme për diastolic (55-115)
    }

    public override string ToString()
    {
        return $"Pacienti: {Name}, Dhome: {RoomNumber}, Doktori: {Doctor}, Tensioni: {Systolic}/{Diastolic} mmHg";
    }
}
