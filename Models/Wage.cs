namespace victors.Models
{
    public class Wage
    {
        public int WageId { get; set; }
        public int StaffId { get; set; }
        public Staff? Staff { get; set; }= new Staff();
        public string Reason { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public decimal NetPay { get; set; }
        public decimal Paye { get; set; }
        public decimal NSSF {  get; set; }=decimal.Zero;
        public decimal Compulserysavings { get; set; }
        public int PayMonth { get; set; }= DateTime.Now.Month;
        public decimal Balance { get; set; }
        
        public string SalaryDate { get; set; } = $"{DateTime.Now.Day}/{DateTime.Now.Month}/{DateTime.Now.Year}";
        public decimal BalanceCalculator(Staff staff, decimal amount, decimal balance)
        {            
            if (Reason.ToLower() == "salary")
            {


                NSSF = (decimal)Math.Floor(0.1 * (double)amount);
                Balance = (staff.Salary - amount) + balance;
                Compulserysavings = (decimal)Math.Floor(0.1 * (double)amount);
                Paye = (decimal)Math.Floor(0.1 * (double)amount);
                 NetPay = (amount - (Compulserysavings+Paye)) ;
               // NetPay = (staff.Salary - (amount));
            }
            if (Reason.ToLower() == "advance")
            {
                Balance = (staff.Salary - Amount) + balance;    

                 NetPay = (staff.Salary - (amount));
            }

            return Balance;
        }
    }
}

