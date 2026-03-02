# Industrial Energy Management System (IEMS)

A simple **ASP.NET MVC 5** project for managing machines and their energy consumption in an industrial setup.  

This project allows users to:

- Add, view, edit, and delete machines.
- Record energy logs for machines, including run hours and energy consumed (kWh).
- Filter energy logs by machine name and date range.
- View calculated energy usage dynamically based on machine power ratings.
- (Future) Generate reports and add authentication/authorization.

---

## **Technologies Used**

- ASP.NET MVC 5 (.NET Framework 4.8)
- Entity Framework 6 (Code First)
- SQL Server (Database)
- HTML, CSS, Bootstrap 4 (for UI)
- Git & GitHub (version control)

---

## **Project Structure**


IndustrialEnergyManagementSystem/
│
├── Controllers/
│ ├── MachineController.cs
│ └── EnergyController.cs
│
├── Models/
│ ├── Machine.cs
│ ├── EnergyRecord.cs
│ └── EnergyLogViewModel.cs
│
├── Views/
│ ├── Machine/
│ └── Energy/
│
├── Scripts/
├── Content/
├── App_Start/
└── Web.config


---

## **Setup Instructions**

1. Clone the repository:

```bash
git clone [https://github.com/YourUsername/IndustrialEnergyManagementSystem.git](https://github.com/toufiq-akash/IndustrialEnergyManagementSystem)

Open the solution in Visual Studio 2026.

Update the connection string in Web.config to your SQL Server database.

Run the project (F5) and navigate:

/Machine → Manage machines

/Energy → Record and view energy logs

(Optional) Use filters on the Energy Logs page to search by machine name or date range.

Sample Data

Machines Table

Machine Name	Power Rating (kW)	Department
Motor	20	Production
Compressor	15	Maintenance
Heater	10	Production
Wool Cutter	45	Maintenance
Pump	8	Maintenance

Energy Logs Table

Machine	Run Hours	Energy (kWh)	Record Date
Motor	3	60	2026-03-03 08:00
Compressor	2	30	2026-03-03 09:00
Heater	5	50	2026-03-02 14:00
Future Improvements

Generate monthly/weekly energy consumption reports.

Add export to Excel/PDF for energy logs.

Implement AJAX filtering and pagination for Energy Logs.

License

This project is for educational purposes and personal learning.
