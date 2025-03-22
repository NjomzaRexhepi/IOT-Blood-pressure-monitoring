using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
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
            if (patient.Systolic >= 180 || patient.Diastolic >= 110)
            {
                SendSMS(patient.Name, "Hypertensive Emergency! Kërko ndihmë mjekësore menjëherë!");
            }
            else if (patient.Systolic >= 160 || patient.Diastolic >= 100)
            {
                SendSMS(patient.Name, "Moderate Hypertension! Këshillohet konsultë me doktorin.");
            }
            else if (patient.Systolic >= 140 || patient.Diastolic >= 90)
            {
                SendSMS(patient.Name, "Mild Hypertension! Kujdes me dietën dhe aktivitetin.");
            }
            else if (patient.Systolic >= 130 || patient.Diastolic >= 85)
            {
                SendSMS(patient.Name, "High Normal Blood Pressure! Mbaj një stil jetese të shëndetshëm.");
            }
            else if (patient.Systolic < 90 || patient.Diastolic < 60)
            {
                SendSMS(patient.Name, "Low Blood Pressure! Kujdes, mund të kesh simptoma si marramendje.");
            }
        }
    }

    static void SendSMS(string patientName, string message)
    {
        Console.WriteLine($"[SMS] Dërgohet tek {patientName}: {message}");
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
        Systolic = random.Next(85, 180);  // Vlera e rastësishme për systolic (85-180)
        Diastolic = random.Next(55, 110); // Vlera e rastësishme për diastolic (55-110)
    }

    public override string ToString()
    {
        return $"Pacienti: {Name}, Dhome: {RoomNumber}, Doktori: {Doctor}, Tensioni: {Systolic}/{Diastolic} mmHg";
    }
}
