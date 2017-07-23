using System;
using System.Drawing;
using System.Windows.Forms;
using FundManager.BL;
using FundManager.Repositories;

namespace FundManager.UI
{
    public partial class FundManager : Form
    {
        private Fund Fund { get; set; }

        public FundManager()
        {
            InitializeComponent();

            Fund = new Fund(new BuilderFactory(), new StockRepository());

            ControlsRefresh();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!IsInputValid())
                return;

            if (radioBond.Checked)
                Fund.AddBond(price: numPrice.Value, quantity: numQuantity.Value);

            if (radioEquity.Checked)
                Fund.AddEquity(price: numPrice.Value, quantity: numQuantity.Value);

            ControlsRefresh();
        }

        private bool IsInputValid()
        {
            if (!radioEquity.Checked && !radioBond.Checked)
            {
                lblValidation.Text = "Please choose stock type.";
                return false;
            }

            if (numPrice.Value <= 0)
            {
                lblValidation.Text = "Price should be greater than zero.";
                return false;
            }

            if (numQuantity.Value <= 0)
            {
                lblValidation.Text = "Quantity should be greater than zero.";
                return false;
            }

            lblValidation.Text = string.Empty;

            return true;
        }

        private void ControlsRefresh()
        {
            gridStocks.DataSource = Fund.GetAllStock();
            gridStocks.Refresh();

            ClearInputs();
            HighlightRows();
            FillTotals();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearInputs();
        }

        private void ClearInputs()
        {
            numPrice.Value = 0;
            numQuantity.Value = 0;
            lblValidation.Text = string.Empty;
            radioEquity.Checked = false;
            radioBond.Checked = false;
        }

        private void HighlightRows()
        {
            foreach (DataGridViewRow row in gridStocks.Rows)
            {
                var stockName = Convert.ToString(row.Cells[0].Value);
                var marketValue = Convert.ToDecimal(row.Cells[3].Value);
                var transactionCost = Convert.ToDecimal(row.Cells[4].Value);

                if (IsMarketValueLessThenZero(marketValue) ||
                    IsTransactionCostGreaterThanTolerance(stockName, transactionCost))
                {
                    row.DefaultCellStyle.BackColor = Color.Red;
                }
            }
        }

        private bool IsTransactionCostGreaterThanTolerance(string stockName, decimal transactionCost)
        {
            return stockName.Contains("Bond")
                ? transactionCost > Fund.BondTolerance
                : transactionCost > Fund.EquityTolerance;
        }

        private bool IsMarketValueLessThenZero(decimal marketValue)
        {
            return marketValue < 0;
        }

        private void FillTotals()
        {
            var bondTotals = Fund.GetBondTotals();
            txtTotalMarketValueBonds.Text = bondTotals.TotalMarketValue.ToString();
            txtTotalNumberBonds.Text = bondTotals.TotalNumbers.ToString();
            txtTotalQuantitiesBonds.Text = bondTotals.TotalQuantity.ToString();
            txtTotalStockWeightBonds.Text = bondTotals.TotalStockWeight.ToString();

            var equityTotals = Fund.GetEquityTotals();
            txtTotalMarketValueEquities.Text = equityTotals.TotalMarketValue.ToString();
            txtTotalNumberEquities.Text = equityTotals.TotalNumbers.ToString();
            txtTotalQuantitiesEquity.Text = equityTotals.TotalQuantity.ToString();
            txtTotalStockWeightEquities.Text = equityTotals.TotalStockWeight.ToString();

            var allTotals = Fund.GeStocksTotals();
            txtTotalMarketValueAll.Text = allTotals.TotalMarketValue.ToString();
            txtTotalNumberAll.Text = allTotals.TotalNumbers.ToString();
            txtTotalQuantitiesAll.Text = allTotals.TotalQuantity.ToString();
            txtTotalStockWeightAll.Text = allTotals.TotalStockWeight.ToString();
        }
    }
}
