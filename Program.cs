using System;
using System.Collections.Generic;
using System.Threading;

class Program
{
    static List<Patient> patients = new List<Patient>
    {
        new Patient("Arben Hoxha", 101, "Dr. Besnik Kelmendi"),
        new Patient("Elira Berisha", 102, "Dr. Arta Krasniqi"),
        new Patient("Luan Gashi", 101, "Dr. Mentor Sadiku"),
        new Patient("Blerim Krasniqi", 104, "Dr. Arta Krasniqi"),
        new Patient("Alma Sadiku", 102, "Dr. Besnik Kelmendi"),
        new Patient("Driton Shala", 104, "Dr. Besnik Kelmendi"),
        new Patient("Gentiana Zeka", 103, "Dr. Arta Krasniqi"),
        new Patient("Florent Rexhepi", 103, "Dr. Mentor Sadiku"),
        new Patient("Arian Salihu", 103, "Dr. Arta Krasniqi"),
        new Patient("Leonora Thaçi", 104, "Dr. Besnik Kelmendi"),
        new Patient("Shpend Ramadani", 102, "Dr. Besnik Kelmendi"),
        new Patient("Fatmire Leka", 105, "Dr. Arta Krasniqi"),
        new Patient("Visar Hoti", 104, "Dr. Mentor Sadiku"),
        new Patient("Rina Begu", 103, "Dr. Arta Krasniqi"),
        new Patient("Granit Morina", 102, "Dr. Besnik Kelmendi")
    };

    static Dictionary<string, string> doctors = new Dictionary<string, string>
    {
        { "Dr. Besnik Kelmendi", "+38349400984" },
        { "Dr. Arta Krasniqi", "+38349302740" },
        { "Dr. Mentor Sadiku", "+38344510848" }
    };

    static void Main()
    {
        // Initial blood pressure generation for all patients
        foreach (var patient in patients)
        {
            patient.MeasureBloodPressure();
            Console.WriteLine(patient);
        }

        // Set up a timer to generate new blood pressure every 5 minutes (300000 ms)
        Timer timer = new Timer(GenerateNewBloodPressure, null, 0, 300000);  // 5 minutes = 300,000 ms

        // Keep the application running to allow the timer to work
        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();
    }

    static void GenerateNewBloodPressure(object state)
    {
        foreach (var patient in patients)
        {
            patient.MeasureBloodPressure();  // Generate new blood pressure readings
            Console.WriteLine("\n-- Blood Pressure Update --");
            Console.WriteLine(patient);  // Display updated blood pressure information

            // Send SMS based on blood pressure classification
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
                    if(patient.Systolic > 160 || patient.Diastolic > 100)
                        SendSMS(patient.Doctor, doctorPhone, message);
                }
                else
                {
                    Console.WriteLine($"[Gabim] Nuk u gjet numri i telefonit për {patient.Doctor}");
                }
            }
        }
    }

    // Simulate sending an SMS
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

    // Method to generate random blood pressure
    public void MeasureBloodPressure()
    {
        Systolic = random.Next(85, 190);  // Random systolic value between 85 and 190
        Diastolic = random.Next(55, 115); // Random diastolic value between 55 and 115
    }

    public override string ToString()
    {
        return $"Pacienti: {Name}, Dhome: {RoomNumber}, Doktori: {Doctor}, Tensioni: {Systolic}/{Diastolic} mmHg";
    }
}
