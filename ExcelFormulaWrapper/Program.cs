// See https://aka.ms/new-console-template for more information
using System.Numerics;
using ExcelFormulaWrapper;

Console.WriteLine("Hello, World!");
var col1 = 3;
var col2 = 8;
var column1 = new Column(col1);
var  column2 = new Column(col2);
/* "IF({{" + column.Index + "}}>{{" + planned.Index + "}},{{" + column.Index + "}}-{{" +
planned.Index + "}},0)" */
var formula = new If(
    @if: new Greater(column1, column2),
    then:new Subtract(column1, column2),
    @else:new Const("0"));

var f= formula.Build();
Console.WriteLine(f);

/*
 $"IF({{{{{factoryNameColumn.Index}}}}}=\"PETRO\"," +
 $"MAX({{{{{plannedColumn.Index}}}}} - {{{{{stockColumn.Index}}}}} - {{{{{productionMonthColumn.Index}}}}}, 0)," +
$"MAX({rhwsSum} + {{{{{plannedColumn.Index}}}}}-{{{{{stockColumn.Index}}}}},0)";
*/
var factoryNameColumn = new Column(1);
var plannedColumn = new Column(2);
var stockColumn = new Column(3);
var productionMonthColumn = new Column(4);
var rwhs = new Const(333);
var formula2 = new If(
    new Equals(factoryNameColumn, new Const("PETRO")),
    new Max(new Subtract(new Subtract(plannedColumn, stockColumn), productionMonthColumn), new Const("0")),
    new Max(new Subtract(new Add(rwhs, plannedColumn), stockColumn), new Const(0))
    );

Console.WriteLine(formula2);
var petro = new Const("Petro");
/*
 $"IF({{{{{factoryNameColumn.Index}}}}}=\"PETRO\"," +
 $"MAX({{{{{plannedColumn.Index}}}}} - {{{{{stockColumn.Index}}}}} - {{{{{productionMonthColumn.Index}}}}}, 0)," +
$"MAX({rhwsSum} + {{{{{plannedColumn.Index}}}}}-{{{{{stockColumn.Index}}}}},0)";
*/
var formula3 = $"IF({factoryNameColumn} = {petro}, MAX({plannedColumn} + {stockColumn} - {productionMonthColumn}, 0), MAX({rwhs} + {plannedColumn} - {stockColumn}, 0))";
Console.WriteLine(formula3);