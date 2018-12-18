var PrintObjectElement = "";
var printname = "";
function findPrinter() {
    var applet = document.jzebra;
    if (applet != null) {
        // Searches for locally installed printer with "zebra" in the name
        applet.findPrinter("zebra");
    }

    // *Note:  monitorFinding() still works but is too complicated and
    // outdated.  Instead create a JavaScript  function called 
    // "jzebraDoneFinding()" and handle your next steps there.
    monitorFinding();
}

function myIP() {
    var vi = "uses java to get the users local ip number";
    var yip2 = java.net.InetAddress.getLocalHost();
    var yip = yip2.getHostAddress();
    alert(yip);
} //end myIP



function findPrinters() {
    var applet = document.jzebra;
    if (applet != null) {
        // Searches for locally installed printer with "zebra" in the name
        applet.findPrinter("\\{dummy printer name for listing\\}");
    }
    monitorFinding2();
}

function print() {
    var applet = document.jzebra;

    if (applet != null) {
        // Send characters/raw commands to applet using "append"
        // Hint:  Carriage Return = \r, New Line = \n, Escape Double Quotes= \"
        applet.setEncoding("UTF-8");
        applet.append("KRB Software");
        applet.append("A590,1600,2,3,1,1,N,\"jZebra " + applet.getVersion() + " sample.html\"\n");
        applet.append("A590,1570,2,3,1,1,N,\"Testing the print() function\"\n");

        applet.append("P1\n");

        // Send characters/raw commands to printer
        applet.print();
    }

    // *Note:  monitorPrinting() still works but is too complicated and
    // outdated.  Instead create a JavaScript  function called 
    // "jzebraDonePrinting()" and handle your next steps there.
    monitorPrinting();

    /**
    *  PHP PRINTING:
    *  // Uses the php `"echo"` function in conjunction with jZebra `"append"` function
    *  // This assumes you have already assigned a value to `"$commands"` with php
    *  document.jZebra.append(<?php echo $commands; ?>);
    */

    /**
    *  SPECIAL ASCII ENCODING
    *  //applet.setEncoding("UTF-8");
    *  applet.setEncoding("Cp1252"); 
    *  applet.append("\xDA");
    *  applet.append(String.fromCharCode(218));
    *  applet.append(chr(218));
    */

}
function Billprint() {
    var applet = document.jzebra;
    useDefaultPrinter();
    if (applet != null) {
        // Send characters/raw commands to applet using "append"
        // Hint:  Carriage Return = \r, New Line = \n, Escape Double Quotes= \"
        applet.setEncoding("UTF-8");
        //applet.append("x1B\x40"); //	reset printer
        applet.append("\x1B\x6B\x01"); // select typeface - 00 serif - 01 sans serif
        //applet.append("\x1B\x33\x10"); // set line spacing MUST BE x35
        //applet.append("\x1B\x77\x01"); // set double height

        $.each(PrintObjectElement, function () {

            applet.append("\n\r");
            applet.append("\n\r");
            applet.append("\n\r");
            applet.append("\n\r");
            applet.append(spaces(20) + Length(50, this.StudentName) + this.BillNo + "\n"); applet.append("\n\r");
            applet.append(spaces(20) + Length(30, this.ClassName) + Length(20, this.SectionName) + this.RollNo + "\n"); applet.append("\n\r");
            applet.append(spaces(20) + Length(50, this.BillFor) + this.BillDate + "\n");
            applet.append("\n\r");
            applet.append("\n\r");
            var i = 1;
            var hLength = this.Children.length;

            var len = BodyLength(11, hLength);
            $.each(this.Children, function () {
                applet.append(spaces(10) + Length(15, i.toString()) + Length(25, this.ItemName) + spaces(20) + this.Amount + "\n");
                i++;
            });
            for (var j = 0; j < len; j++) {
                applet.append("\n\r");
            }
            applet.append("\x1B\x33\x20");  //	Line spacing
            applet.append(spaces(70) + this.Total + "\n"); applet.append("\x1B\x33\x20");
            applet.append(spaces(70) + this.EducationTax + "\n"); applet.append("\x1B\x33\x20");
            applet.append(spaces(70) + this.Amount + "\n"); applet.append("\x1B\x33\x20");
            applet.append(spaces(30) + this.AmountInWord + "\n");
            applet.append(spaces(75) + this.PreparedBy + "\n");
            applet.append("\n\r");
            applet.append("\n\r");

            applet.print();
            applet.append(chr(27) + chr(105));

        });
    }

    monitorPrinting();
}
function PrintHeader(elements) {
    var applet = "";
    applet.append(spaces(10) + elements.Header.CompanyName + "\n");
    applet.append(spaces(10) + elements.Header.CompanyAddress + "\n");
    applet.append(spaces(10) + "Ph.: " + elements.Header.Phone + "\n");

    applet.append(spaces(10) + Length(55, "Email : " + elements.Header.Email.toString()) + "\n");
    applet.append(spaces(10) + "Pan No.: " + elements.Header.PanNo + "\n");
    applet.append(spaces(40) + "BILL" + "\n");
}


function BothSideBillprint() {
    var applet = document.jzebra;
    useDefaultPrinter();
    if (applet != null) {
        // Send characters/raw commands to applet using "append"
        // Hint:  Carriage Return = \r, New Line = \n, Escape Double Quotes= \"
        applet.setEncoding("UTF-8");
        //applet.append("x1B\x40"); //	reset printer
        //applet.append("\x1B\x6B\x01"); // select typeface - 00 serif - 01 sans serif
        //  applet.append("\x1B\x33\x10"); // set line spacing MUST BE x35
        //applet.append("\x1B\x77\x01"); // set double height
        // applet.append("\x1B\x40"); // 5


        applet.append("\x1B\x40"); // 1
        applet.append("\x1B\x6B\x01");
        applet.append("\x1B\x21\x01"); // 3

        //         applet.append(" International \r\n");
        //         applet.append(" Company \r\n");
        //         applet.append("\x1B\x21\x01"); // 3
        //         applet.append(" ************************************************** \r\n");
        //         applet.append("Info: 42972\r\n");
        //         applet.append("Info: Kommm\r\n");
        //         applet.append("Datum: 14:00 01/02\r\n");
        //         applet.append(" -------------------------------------------------- \r\n");
        //         applet.append("Info: 42972\r\n");
        //         applet.append("Info: Kommm\r\n");
        //         applet.append("Datum: 14:00 01/02\r\n");
        //         applet.append(" -------------------------------------------------- \r\n");
        //         applet.append(" \r\n");
        //         applet.append(" \r\n");
        //         applet.append(" \r\n");
        //         applet.append(" \r\n");
        //         applet.append("\x1D\x56\x41"); // 4


        $.each(PrintObjectElement, function () {

            applet.append("|" + doubledash(42) + "|" + spaces(4) + "|" + doubledash(42) + "|");
            // Print Header
            applet.append("\n");
            applet.append("|" + Length(42, Length(22, spaces(1) + "Estd :") + textright(20, "Phone : " + this.Header.Phone)) + "|" + spaces(4) + "|" + Length(42, Length(22, spaces(1) + "Estd :") + textright(20, "Phone.: " + this.Header.Phone)) + "|" + "\n");

            applet.append("|" + Length(42, spaces(42)) + "|" + spaces(4) + "|" + Length(42, spaces(42)) + "|");
            applet.append("\n");

            applet.append("|" + Length(42, textcenter(42, this.Header.CompanyName)) + "|" + spaces(4) + "|" + Length(42, textcenter(42, this.Header.CompanyName)) + "|" + "\n\r");
            applet.append("|" + Length(42, textcenter(42, this.Header.CompanyAddress)) + "|" + spaces(4) + "|" + Length(42, textcenter(42, this.Header.CompanyAddress)) + "|" + "\n\r");
            applet.append("|" + Length(42, spaces(1) + "OFFICE COPY") + "|" + spaces(4) + "|" + Length(42, spaces(1) + "CUSTOMER COPY") + "|" + "\n");

            //  applet.append(spaces(10) + Length(55, "Email : " + this.Header.Email.toString()) + "\n");
            //           applet.append(spaces(10) + "Pan No.: " + this.Header.PanNo + "\n");
            applet.append("|" + Length(42, textcenter(42, "RECEIPT/PAYMENT")) + "|" + spaces(4) + "|" + Length(42, textcenter(42, "RECEIPT/PAYMENT")) + "|" + "\n");
            //header end

            //            applet.append("|" + Length(42, spaces(42)) + "|" + spaces(4) + "|" + Length(42, spaces(42)) + "|");
            //            applet.append("\n");
            applet.append("|" + doubledash(42) + "|" + spaces(4) + "|" + doubledash(42) + "|");
            applet.append("\n");
            var sectionname = "";
            if (this.SectionName != "") {
                sectionname = "(" + this.SectionName + ")";
            }
            applet.append("|" + Length(42, Length(25, spaces(1) + "Bill No.:" + this.BillNo) + "Date.: " + this.BillDate) + "|" + spaces(4) + "|" + Length(42, Length(25, spaces(1) + "Bill No.:" + this.BillNo) + "Date.: " + this.BillDate) + "|" + "\n");
            applet.append("|" + Length(42, " Name:" + this.StudentName + " [RN:" + this.RollNo + ",Class:" + this.ClassName + sectionname + "]") + "|" + spaces(4) + "|" + Length(42, " Name:" + this.StudentName + " [RN:" + this.RollNo + ",Class:" + this.ClassName + sectionname + "]") + "|" + "\n");
            //            applet.append("|" + Length(42, spaces(42)) + "|" + spaces(4) + "|" + Length(42, spaces(42)) + "|");
            //            applet.append("\n");
            applet.append("|" + dash(42) + "|" + spaces(4) + "|" + dash(42) + "|"); applet.append("\n");
            applet.append("|" + Length(42, Length(5, spaces(1) + "S.N") + " | " + Length(23, textcenter(23, "Fee Description")) + " | Amount ") + "|" + spaces(4) + "|" + Length(42, Length(5, spaces(1) + "S.N") + " | " + Length(23, textcenter(23, "Fee Description")) + " | Amount") + "|" + "\n");
            applet.append("|" + dash(42) + "|" + spaces(4) + "|" + dash(42) + "|");
            applet.append("\n");
            var amount = 0;
            var i = 1;
            $.each(this.Children, function () {

                applet.append("|" + Length(42, Length(5, spaces(2) + i.toString()) + " | " + Length(23, this.ItemName) + " | " + this.Amount) + "|" + spaces(4) + "|" + Length(42, Length(5, spaces(2) + i.toString()) + " | " + Length(23, this.ItemName) + " | " + this.Amount) + "|" + "\n");

                amount += this.Amount;
                i++;
            });
            applet.append("|" + dash(42) + "|" + spaces(4) + "|" + dash(42) + "|");
            applet.append("\n");

            applet.append("|" + Length(42, textright(34, "Total | ") + this.Total) + "|" + spaces(4) + "|" + Length(42, textright(34, "Total | ") + this.Total) + "|" + "\n");
            applet.append("|" + Length(42, textright(34, "Education Tax 1% | ") + this.EducationTax) + "|" + spaces(4) + "|" + Length(42, textright(34, "Education Tax 1% | ") + this.EducationTax) + "|" + "\n");
            applet.append("|" + dash(42) + "|" + spaces(4) + "|" + dash(42) + "|");
            applet.append("\n");
            applet.append("|" + Length(42, textright(34, "Grand Total | ") + this.Amount) + "|" + spaces(4) + "|" + Length(42, textright(34, "Grand Total | ") + this.Amount) + "|" + "\n");
            applet.append("|" + dash(42) + "|" + spaces(4) + "|" + dash(42) + "|"); applet.append("\n");

            applet.append("|" + Length(42, spaces(1) + "Rupees :" + this.AmountInWord) + "|" + spaces(4) + "|" + Length(42, spaces(1) + "Rupees :" + this.AmountInWord) + "|" + "\n");

            applet.append("|" + Length(42, spaces(42)) + "|" + spaces(4) + "|" + Length(42, spaces(42)) + "|");
            applet.append("\n");
            applet.append("|" + textright(42, "Cashier Signature") + "|" + spaces(4) + "|" + textright(42, "Cashier Signature") + "|" + "\n");
            applet.append("|" + doubledash(42) + "|" + spaces(4) + "|" + doubledash(42) + "|");
            //               

            // applet.append(spaces(10) + Length(50, "Name : " + this.StudentName) + "Bill No.: " + this.BillNo + "\n");

            //  applet.append("\n\r");
            //   applet.append(spaces(10) + Length(30, "Class : " + this.ClassName) + Length(20, "Section : " + this.SectionName) + "Roll No. : " + this.RollNo + "\n"); applet.append("\n\r");
            //      applet.append(spaces(10) + Length(50, "Month of : " + this.BillFor) + "Bill Date : " + this.BillDate + "\n");
            //            applet.append("\n\r");
            //            applet.append(dash(56));

            //            applet.append("\n\r");
            //            applet.append(spaces(7) + Length(15, "S.N") + Length(45, "Fee Description") + "Amount" + "\n");
            //            applet.append(dash(56));
            //            applet.append("\n\r");
            //            var i = 1;
            //            var hLength = this.Children.length;
            //            var amount = 0;
            //            var len = BodyLength(11, hLength);
            //            $.each(this.Children, function () {
            //                applet.append(spaces(10) + Length(15, i.toString()) + Length(45, this.ItemName) + this.Amount + "\n");
            //                amount += this.Amount;
            //                i++;
            //            });
            //            for (var j = 0; j < len; j++) {
            //                applet.append("\n\r");
            //            }
            //            applet.append("\x1B\x33\x20");  //	Line spacing
            //            applet.append(spaces(25) + Length(45, "Total") + this.Total + "\n"); applet.append("\x1B\x33\x20");
            //            applet.append(spaces(25) + Length(45, "Education Tax 1%") + this.EducationTax + "\n"); applet.append("\x1B\x33\x20");
            //            applet.append(spaces(25) + Length(45, "Net Amount") + this.Amount + "\n"); applet.append("\x1B\x33\x20");
            //            applet.append(spaces(10) + "Rupees in word : " + this.AmountInWord + "\n");
            //            applet.append(spaces(75) + this.PreparedBy + "\n");
            applet.append("\n\r");
            applet.append("\n\r");
            applet.append("\n\r");
            applet.append("\n\r");
            applet.print();
            applet.append(chr(27) + chr(105));

        });
    }

    monitorPrinting();
}
function DonboscoBillprint() {
    var applet = document.jzebra;
    useDefaultPrinter();
    if (applet != null) {
        // Send characters/raw commands to applet using "append"
        // Hint:  Carriage Return = \r, New Line = \n, Escape Double Quotes= \"
        applet.setEncoding("UTF-8");
        //applet.append("x1B\x40"); //	reset printer
        applet.append("\x1B\x6B\x01"); // select typeface - 00 serif - 01 sans serif
        //applet.append("\x1B\x33\x10"); // set line spacing MUST BE x35
        //applet.append("\x1B\x77\x01"); // set double height

        $.each(PrintObjectElement, function () {
            applet.append(spaces(20) + Length(50, this.BillFor) + this.BillDate + "\n"); applet.append("\n\r");
            //            applet.append("\n\r");
            //            applet.append("\n\r");
            //            applet.append("\n\r");
            //            applet.append("\n\r");
            //            applet.append(spaces(20) + Length(50, this.StudentName) + this.BillNo + "\n"); applet.append("\n\r");
            //            applet.append(spaces(20) + Length(30, this.ClassName) + Length(20, this.SectionName) + this.RollNo + "\n"); applet.append("\n\r");
            //            applet.append(spaces(20) + Length(50, this.BillFor) + this.BillDate + "\n");
            //            applet.append("\n\r");
            //            applet.append("\n\r");
            //            var i = 1;
            //            var hLength = this.Children.length;

            //            var len = BodyLength(11, hLength);
            //            $.each(this.Children, function () {
            //                applet.append(spaces(10) + Length(15, i.toString()) + Length(25, this.ItemName) + spaces(20) + this.Amount + "\n");
            //                i++;
            //            });
            //            for (var j = 0; j < len; j++) {
            //                applet.append("\n\r");
            //            }
            //            applet.append("\x1B\x33\x20");  //	Line spacing
            //            applet.append(spaces(70) + this.Total + "\n"); applet.append("\x1B\x33\x20");
            //            applet.append(spaces(70) + this.EducationTax + "\n"); applet.append("\x1B\x33\x20");
            //            applet.append(spaces(70) + this.Amount + "\n"); applet.append("\x1B\x33\x20");
            //            applet.append(spaces(30) + this.AmountInWord + "\n");
            //            applet.append(spaces(75) + this.PreparedBy + "\n");
            //            applet.append("\n\r");
            //            applet.append("\n\r");

            //            applet.print();
            //            applet.append(chr(27) + chr(105));

        });
    }

    monitorPrinting();
}
function BillHtmlprint() {
    var applet = document.jzebra;
    useDefaultPrinter();
    if (applet != null) {
        // Send characters/raw commands to applet using "append"
        // Hint:  Carriage Return = \r, New Line = \n, Escape Double Quotes= \"
        applet.setEncoding("UTF-8");
        //applet.append("x1B\x40"); //	reset printer
        applet.append("\x1B\x6B\x01"); // select typeface - 00 serif - 01 sans serif
        //applet.append("\x1B\x33\x10"); // set line spacing MUST BE x35
        //applet.append("\x1B\x77\x01"); // set double height

        $.each(PrintObjectElement, function () {


            // Print Header
            applet.append(spaces(10) + this.Header.CompanyName + "\n");
            applet.append(spaces(10) + this.Header.CompanyAddress + "\n");
            applet.append(spaces(10) + "Ph.: " + this.Header.Phone + "\n");

            applet.append(spaces(10) + Length(55, "Email : " + this.Header.Email.toString()) + "\n");
            applet.append(spaces(10) + "Pan No.: " + this.Header.PanNo + "\n");
            applet.append(spaces(40) + "BILL" + "\n");
            //header end
            applet.append(dash(56));


            applet.append("\n\r");
            applet.append(spaces(10) + Length(50, "Name : " + this.StudentName) + "Bill No.: " + this.BillNo + "\n");
            applet.append("\n\r");
            applet.append(spaces(10) + Length(30, "Class : " + this.ClassName) + Length(20, "Section : " + this.SectionName) + "Roll No. : " + this.RollNo + "\n"); applet.append("\n\r");
            applet.append(spaces(10) + Length(50, "Month of : " + this.BillFor) + "Bill Date : " + this.BillDate + "\n");
            applet.append("\n\r");

            applet.append(dash(56));
            applet.append("\n\r");
            applet.append(spaces(7) + Length(15, "S.N") + Length(45, "Fee Description") + "Amount" + "\n");
            applet.append(dash(56));
            applet.append("\n\r");
            var i = 1;
            var hLength = this.Children.length;
            var amount = 0;
            var len = BodyLength(11, hLength);
            $.each(this.Children, function () {
                applet.append(spaces(10) + Length(15, i.toString()) + Length(45, this.ItemName) + this.Amount + "\n");
                amount += this.Amount;
                i++;
            });
            for (var j = 0; j < len; j++) {
                applet.append("\n\r");
            }
            applet.append("\x1B\x33\x20");  //	Line spacing
            applet.append(spaces(25) + Length(45, "Total") + this.Total + "\n"); applet.append("\x1B\x33\x20");
            applet.append(spaces(25) + Length(45, "Education Tax 1%") + this.EducationTax + "\n"); applet.append("\x1B\x33\x20");
            applet.append(spaces(25) + Length(45, "Net Amount") + this.Amount + "\n"); applet.append("\x1B\x33\x20");
            applet.append(spaces(10) + "Rupees in word : " + this.AmountInWord + "\n");
            applet.append(spaces(75) + this.PreparedBy + "\n");
            applet.append("\n\r");
            applet.append("\n\r");

            applet.print();
            applet.append(chr(27) + chr(105));

        });
    }

    monitorPrinting();
}
function DonboscoBillHtmlprint() {
    var applet = document.jzebra;
    useDefaultPrinter();
    if (applet != null) {
        // Send characters/raw commands to applet using "append"
        // Hint:  Carriage Return = \r, New Line = \n, Escape Double Quotes= \"
        applet.setEncoding("UTF-8");
        applet.append("\x1B\x40"); // 1
        applet.append("\x1B\x6B\x01");
        applet.append("\x1B\x21\x01"); // 3

        //applet.append("x1B\x40"); //	reset printer
        //        applet.append("\x1B\x6B\x01"); // select typeface - 00 serif - 01 sans serif
        //        applet.append("\x1D\x56\x41"); // 4
        //applet.append("\x1B\x33\x10"); // set line spacing MUST BE x35
        //applet.append("\x1B\x77\x01"); // set double height
        //         applet.append(" International \r\n");
        //         applet.append(" Company \r\n");
        //         applet.append("\x1B\x21\x01"); // 3
        //         applet.append(" ************************************************** \r\n");
        //         applet.append("Info: 42972\r\n");
        //         applet.append("Info: Kommm\r\n");
        //         applet.append("Datum: 14:00 01/02\r\n");
        //         applet.append(" -------------------------------------------------- \r\n");
        //         applet.append("Info: 42972\r\n");
        //         applet.append("Info: Kommm\r\n");
        //         applet.append("Datum: 14:00 01/02\r\n");
        //         applet.append(" -------------------------------------------------- \r\n");
        //         applet.append(" \r\n");
        //         applet.append(" \r\n");
        //         applet.append(" \r\n");
        //         applet.append(" \r\n");
        //         applet.append("\x1D\x56\x41"); // 4
        $.each(PrintObjectElement, function () {


            // Print Header
            //            applet.append(spaces(10) + this.Header.CompanyName + "\n");
            //            applet.append(spaces(10) + this.Header.CompanyAddress + "\n");
            //            applet.append(spaces(10) + "Ph.: " + this.Header.Phone + "\n");


            //            applet.append(spaces(40) + "BILL" + "\n");

            // Print Header

            applet.append(Length(90, Length(50, spaces(1) + "Estd :") + textright(40, "Phone : " + this.Header.Phone)) + "\n\r");
            applet.append(Length(90, textcenter(90, this.Header.CompanyName)) + "\n\r");
            applet.append(Length(90, textcenter(90, this.Header.CompanyAddress)) + "\n\r");
            applet.append(Length(90, textcenter(90, "Email : " + this.Header.Email.toString())) + "\r\n");
            applet.append(Length(90, textcenter(90, "Pan No.: " + this.Header.PanNo.toString())) + "\r\n");
            //header end
            applet.append(dash(90));
            applet.append("\n\r");
            applet.append(Length(90, Length(50, spaces(1) + "Name : " + this.StudentName) + textright(40, "Bill No.: " + this.BillNo)) + "\n\r");
            applet.append(Length(90, Length(30, spaces(1) + "Class : " + this.ClassName) + textcenter(30, "Section : " + this.SectionName) + textright(30, "Roll No. : " + this.RollNo)) + "\n\r");
            applet.append(Length(90, Length(50, spaces(1) + "Month of : " + this.BillFor) + textright(40, "Bill Date : " + this.BillDate)) + "\n");
            applet.append(dash(90));
            applet.append("\n\r");
            applet.append(Length(90, spaces(1) + textcenter(4, "S.N") + textcenter(21, "Fee Description") + textcenter(14, "Fee Amount") + textcenter(14, "Term Amount") + textcenter(14, "Net Amount") + textcenter(14, "Tax Amount") + textcenter(8, "Total")) + "\n\r");
            applet.append(dash(90));
            applet.append("\n\r");
            var i = 1;
            var hLength = this.Children.length;
            var amount = 0;
            var TermAmount = 0;
            var NetAmount = 0;
            var Taxamount = 0;
            var Total = 0;
            var grantotal = 0;
            var len = BodyLength(15, hLength);
            $.each(this.Children, function () {
                // applet.append(spaces(10) + Length(15, i.toString()) + Length(45, this.ItemName) + this.Amount + "\n");
                applet.append(Length(90, spaces(2) + Length(5, i.toString()) + Length(21, this.ItemName) + Length(12, this.Amount.toString()) + Length(14, this.TermAmount.toString()) + Length(14, this.NetAmount.toString()) + Length(14, this.TaxAmount.toString()) + Length(8, this.Total.toString())) + "\n\r");
                amount += this.Amount;
                TermAmount += this.TermAmount;
                NetAmount += this.NetAmount;
                Taxamount += this.Taxamount;
                Total += this.Total;
                grantotal += this.Total;
                i++;
            });
            applet.append(dash(90));
            applet.append("\n\r");
            applet.append(Length(90, textleft(20, spaces(1) + "Previous Amount") + textright(70, this.DueAmount.toString() + spaces(2))));
            grantotal += this.DueAmount;
            applet.append("\n\r");
            applet.append(textright(90, dash(10)));
            applet.append("\n\r");
            applet.append(Length(90, textleft(20, spaces(1) + "Total") + textright(70, grantotal.toString() + spaces(2))) + "\n\r");
            applet.append(textright(90, doubledash(10)));
            applet.append("\n\r");

            //            applet.append("\x1B\x33\x20");  //	Line spacing
            // applet.append(spaces(25) + Length(45, "Total") + this.Total + "\n"); applet.append("\x1B\x33\x20");
            //  applet.append(spaces(25) + Length(45, "Education Tax 1%") + this.EducationTax + "\n"); applet.append("\x1B\x33\x20");
            //   applet.append(spaces(25) + Length(45, "Net Amount") + this.Amount + "\n"); applet.append("\x1B\x33\x20");
            applet.append(Length(90, Length(75, spaces(1) + "Rupees in word : " + this.AmountInWord) + textright(15, this.PreparedBy) + "\n\r"));

            for (var j = 0; j < len; j++) {
                applet.append("\n\r");
            }
            applet.append(dash(90));
            applet.append("\n\r"); applet.append("\n\r"); applet.append("\n\r");
            //            applet.append("\n\r");
            //            applet.append("\n\r");
            //            applet.append("\n\r");
            //            applet.append(dash(90));
            //            applet.append("\n\r");
            //            applet.append("\n\r");
            applet.print();
            applet.append(chr(27) + chr(105));

        });
    }

    monitorPrinting();
}
function BodyLength(htlen, hLength) {
    var len = htlen - hLength;
    return len;
}
function FeeReceiptprint() {
    var applet = document.jzebra;
    useDefaultPrinter();
    if (applet != null) {
        // Send characters/raw commands to applet using "append"
        // Hint:  Carriage Return = \r, New Line = \n, Escape Double Quotes= \"
        $.each(PrintObjectElement, function () {

            applet.setEncoding("UTF-8");
            //applet.append("x1B\x40"); //	reset printer
            applet.append("\x1B\x6B\x01"); // select typeface - 00 serif - 01 sans serif
            //applet.append("\x1B\x33\x10"); // set line spacing MUST BE x35
            //applet.append("\x1B\x77\x01"); // set double height
            applet.append(spaces(65) + this.Date + "\n\r" + spaces(65) + this.ReceiptNo);

            applet.append("\n\r");
            applet.append("\n\r");
            applet.append("\x1B\x33\x20"); //	Line spacing
            applet.append(spaces(65) + this.Name + "\n");

            applet.append("\n\r");
            applet.append(spaces(25) + Length(20, this.ClassName) + spaces(15) + Length(20, this.SectionName) + spaces(15) + Length(20, this.RollNo) + "\n");

            applet.append("\n\r");
            applet.append(spaces(45) + this.PaymentFor + "\n");

            applet.append("\n\r");
            applet.append(spaces(25) + this.InWords + "\n");

            applet.append("\n\r");
            applet.append(spaces(25) + this.Amount + "\n" + spaces(65) + this.ReceivedBy + "\n");

            applet.append("\n\r");
            applet.append("\n\r");
            applet.print();
            applet.append(chr(27) + chr(105));

        });
    }

    // *Note:  monitorPrinting() still works but is too complicated and
    // outdated.  Instead create a JavaScript  function called 
    // "jzebraDonePrinting()" and handle your next steps there.
    monitorPrinting();

    /**
    *  PHP PRINTING:
    *  // Uses the php `"echo"` function in conjunction with jZebra `"append"` function
    *  // This assumes you have already assigned a value to `"$commands"` with php
    *  document.jZebra.append(<?php echo $commands; ?>);
    */

    /**
    *  SPECIAL ASCII ENCODING
    *  //applet.setEncoding("UTF-8");
    *  applet.setEncoding("Cp1252"); 
    *  applet.append("\xDA");
    *  applet.append(String.fromCharCode(218));
    *  applet.append(chr(218));
    */

}
function DonboscoFeeReceiptprint() {
    var applet = document.jzebra;
    useDefaultPrinter();
    if (applet != null) {
        // Send characters/raw commands to applet using "append"
        // Hint:  Carriage Return = \r, New Line = \n, Escape Double Quotes= \"
        $.each(PrintObjectElement, function () {

            applet.setEncoding("UTF-8");
            //applet.append("x1B\x40"); //	reset printer
            applet.append("\x1B\x6B\x01"); // select typeface - 00 serif - 01 sans serif
            //applet.append("\x1B\x33\x10"); // set line spacing MUST BE x35
            // applet.append("\x1B\x77\x01"); // set double height
            applet.append("\n\r");
            applet.append("\n\r");
            applet.append("\n\r");
            applet.append("\n\r");
            applet.append(spaces(140) + this.ReceiptNo);
            applet.append("\n\r");
            applet.append("\n\r");
            applet.append(spaces(140) + this.Date);
            applet.append("\n\r");
            applet.append("\n\r");
            //A.Cno

            applet.append("\n\r");
            applet.append("\n\r");
            applet.append("\x1B\x33\x10"); //	Line spacing
            applet.append(spaces(50) + Length(60, this.Name) + this.Amount);
            applet.append("\n\r");
            applet.append("\n\r");
            applet.append(spaces(30) + Length(100, this.InWords));
            applet.append("\n\r");
            applet.append("\n\r");
            applet.append(spaces(25) + this.ClassName);
            applet.append("\n\r");
            applet.append("\n\r");
            //            applet.append(spaces(45) + this.PaymentFor + "\n");

            //            applet.append("\n\r");
            //            applet.append(spaces(25) + this.InWords + "\n");

            //            applet.append("\n\r");
            //            applet.append(spaces(25) + this.Amount + "\n" + spaces(65) + this.ReceivedBy + "\n");

            applet.append("\n\r");
            applet.append("\n\r");
            applet.print();
            applet.append(chr(27) + chr(105));

        });
    }

    // *Note:  monitorPrinting() still works but is too complicated and
    // outdated.  Instead create a JavaScript  function called 
    // "jzebraDonePrinting()" and handle your next steps there.
    monitorPrinting();

    /**
    *  PHP PRINTING:
    *  // Uses the php `"echo"` function in conjunction with jZebra `"append"` function
    *  // This assumes you have already assigned a value to `"$commands"` with php
    *  document.jZebra.append(<?php echo $commands; ?>);
    */

    /**
    *  SPECIAL ASCII ENCODING
    *  //applet.setEncoding("UTF-8");
    *  applet.setEncoding("Cp1252"); 
    *  applet.append("\xDA");
    *  applet.append(String.fromCharCode(218));
    *  applet.append(chr(218));
    */

}
function FeeReceiptHtmlprint() {
    var applet = document.jzebra;
    useDefaultPrinter();
    if (applet != null) {
        // Send characters/raw commands to applet using "append"
        // Hint:  Carriage Return = \r, New Line = \n, Escape Double Quotes= \"

        $.each(PrintObjectElement, function () {
            applet.setEncoding("UTF-8");
            //applet.append("x1B\x40"); //	reset printer
            applet.append("\x1B\x6B\x01"); // select typeface - 00 serif - 01 sans serif
            //applet.append("\x1B\x33\x10"); // set line spacing MUST BE x35
            // applet.append("\x1B\x77\x01"); // set double height


            // Print Header
            applet.append(spaces(10) + this.ReportHeader.CompanyName + "\n");
            applet.append(spaces(10) + this.ReportHeader.CompanyAddress + "\n");
            applet.append(spaces(10) + "Ph.: " + this.ReportHeader.Phone + "\n");

            applet.append(spaces(10) + Length(55, "Email : " + this.ReportHeader.Email.toString()) + "Date :" + this.Date + "\n");
            applet.append(spaces(65) + "Receipt No. :" + this.ReceiptNo + "\n");
            applet.append(spaces(40) + "RECEIPT" + "\n");
            //header end
            //
            dash(56);


            applet.append("\n\r");
            applet.append("\n\r");
            applet.append("\x1B\x33\x20"); //	Line spacing
            applet.append(spaces(10) + "Received with thanks form Ms./Mr.: " + this.Name + "\n");

            applet.append("\n\r");
            applet.append(spaces(10) + "Class : " + Length(30, this.ClassName) + "Section : " + Length(10, this.SectionName) + "Roll No. :" + this.RollNo + "\n");

            applet.append("\n\r");
            applet.append(spaces(10) + "For the Payment Of : " + this.PaymentFor + "\n");

            applet.append("\n\r");
            applet.append(spaces(10) + "In words : " + this.InWords + "\n");

            applet.append("\n\r");
            applet.append(spaces(10) + "Rs. : " + this.Amount + "\n" + spaces(65) + "Received By : " + this.ReceivedBy + "\n");

            applet.append("\n\r");
            applet.append("\n\r");
            applet.print();
            applet.append(chr(27) + chr(105));

        });
    }

    monitorPrinting();


}
function DonboscoFeeReceiptHtmlprint() {
    var applet = document.jzebra;
    useDefaultPrinter();
    if (applet != null) {
        // Send characters/raw commands to applet using "append"
        // Hint:  Carriage Return = \r, New Line = \n, Escape Double Quotes= \"

        $.each(PrintObjectElement, function () {
            applet.setEncoding("UTF-8");
            applet.append("\x1B\x40"); // 1
            applet.append("\x1B\x6B\x01");
            applet.append("\x1B\x21\x01"); // 3
            //applet.append("\x1D\x56\x41"); // 4
            // Print Header
            //            applet.append(spaces(10) + this.ReportHeader.CompanyName + "\n");
            //            applet.append(spaces(10) + this.ReportHeader.CompanyAddress + "\n");
            //            applet.append(spaces(10) + "Ph.: " + this.ReportHeader.Phone + "\n");
            applet.append("\n\r"); applet.append("\n\r");
            applet.append(Length(90, Length(60, spaces(3) + this.ReportHeader.CompanyName) + textright(30,"Receipt No. "+ this.ReceiptNo)) + "\n");
            applet.append(Length(90, Length(70, spaces(3) + this.ReportHeader.CompanyAddress) + textright(20, "Date :" + this.Date)) + "\n");
            applet.append(Length(90, spaces(3) + "Pan No.: " + this.ReportHeader.PanNo) + "\n");
            applet.append(Length(90, spaces(3) + "Ph.: " + this.ReportHeader.Phone) + "\n");
            applet.append(Length(90, spaces(3) + "Email: " + this.ReportHeader.Email) + "\n"); applet.append("\n\r");
            applet.append(textcenter(90, "RECEIPT") + "\n");
            //header end
            //
            applet.append(dash(90));
            applet.append("\n\r");
            applet.append(Length(90, Length(70, spaces(3) + "Received with thanks form Ms./Mr.: " + this.Name) + textright(20, "a sum of Rs. " + this.Amount)) + "\n\r");
            applet.append("\n\r");
            applet.append(Length(90, spaces(3) + "In words Rs. : " + this.InWords) + "\n\r");
            applet.append("\n\r");
            applet.append(Length(90, Length(30, spaces(3) + "Class : " + this.ClassName) + Length(30, "Section : " + this.SectionName) + Length(30, "Roll No. :" + this.RollNo)) + "\n\r");
            applet.append("\n\r");
            applet.append(textcenter(18, "Pre Balance") + textcenter(18, "Amount Paid") + textcenter(18, "Balance Due") + "\n\r");
            applet.append(textcenter(18, this.PreBalance.toString()) + textcenter(18, this.Amount.toString()) + textcenter(18, this.BalanceDue.toString()) + "\n\r");

            applet.append(Length(75, " ") + Length(15, "Cashier") + "\n\r");
            // applet.append(spaces(10) + "For the Payment Of : " + this.PaymentFor + "\n");

            //            applet.append("\n\r");
            //            applet.append(spaces(10) + "In words : " + this.InWords + "\n");

            //            applet.append("\n\r");
            //            applet.append(spaces(10) + "Rs. : " + this.Amount + "\n" + spaces(65) + "Received By : " + this.ReceivedBy + "\n");

            applet.append("\n\r");
            applet.append("\n\r");
            applet.append("\n\r");
            applet.append("\n\r");
           
            applet.append(chr(27) + chr(105)); applet.print();

        }); 

    }

    monitorPrinting();


}
function PaymentSlipPrint() {
    var applet = document.jzebra;
    useDefaultPrinter();
    if (applet != null) {
        // Send characters/raw commands to applet using "append"
        // Hint:  Carriage Return = \r, New Line = \n, Escape Double Quotes= \"
        applet.setEncoding("UTF-8");
        //applet.append("x1B\x40"); //	reset printer
        applet.append("\x1B\x6B\x01"); // select typeface - 00 serif - 01 sans serif
        //applet.append("\x1B\x33\x10"); // set line spacing MUST BE x35
        //applet.append("\x1B\x77\x01"); // set double height
        applet.append(spaces(150) + PrintObjectElement.Date + "\n\r" + spaces(150) + PrintObjectElement.SlipNo);

        applet.append("\n\r");
        applet.append("\n\r");
        applet.append("\x1B\x33\x20");  //	Line spacing
        applet.append(spaces(65) + PrintObjectElement.Name + "\n");



        applet.append("\n\r");
        applet.append(spaces(45) + PrintObjectElement.PaymentFor + "\n");

        applet.append("\n\r");
        applet.append(spaces(25) + PrintObjectElement.InWords + "\n");

        applet.append("\n\r");
        applet.append(spaces(25) + PrintObjectElement.Amount + "\n" + spaces(150) + PrintObjectElement.ReceivedBy + "\n");

        applet.append("\n\r");
        applet.append("\n\r");
        applet.print();
        applet.append(chr(27) + chr(105));


    }

    // *Note:  monitorPrinting() still works but is too complicated and
    // outdated.  Instead create a JavaScript  function called 
    // "jzebraDonePrinting()" and handle your next steps there.
    monitorPrinting();

    /**
    *  PHP PRINTING:
    *  // Uses the php `"echo"` function in conjunction with jZebra `"append"` function
    *  // This assumes you have already assigned a value to `"$commands"` with php
    *  document.jZebra.append(<?php echo $commands; ?>);
    */

    /**
    *  SPECIAL ASCII ENCODING
    *  //applet.setEncoding("UTF-8");
    *  applet.setEncoding("Cp1252"); 
    *  applet.append("\xDA");
    *  applet.append(String.fromCharCode(218));
    *  applet.append(chr(218));
    */

}
function textcenter(totallen, text) {
    var rem = text.length % 2;
    var textlen = text.length;
    //    if (rem != 0) {
    //        textlen += 1;
    //    }
    //    if (text.length > totallen) {
    //        var list = text.split(' ');
    //        var textvalue;
    //        $.each(list, function (index) {
    //            textvalue += this;
    //            if (index == 3) {
    //               var len1 = (totallen - textvalue.length) / 2;
    //                textvalue = spaces(len1) + textvalue + spaces(len1) + "|"+"\n";
    //            }

    //        });
    //    } else {
    var len = (totallen - textlen) / 2;
    return spaces(len) + text + spaces(len);
    //}



}
function textright(totallen, text) {
    var len = (totallen - text.length);
    return spaces(len) + text;




}
function textleft(totallen, text) {
    var len = (totallen - text.length);
    return text + spaces(len);




}
function dash(totallen) {
    var str = "";
    for (var i = 1; i <= totallen; i++) {
        str += "-";
    }
    return str;
}
function doubledash(totallen) {
    var str = "";
    for (var i = 1; i <= totallen; i++) {
        str += "=";
    }
    return str;
}
function bold() {
    document.jzebra.append(chr(27) + chr(69) + "\r");  // bold on
    // document.jzebra.append(chr(27) + "\x61" + "\x31"); // center justify
}
function center() {
    document.jzebra.append(chr(27) + "\x61" + "\x31"); // center justify
}
function Length(totallen, text1) {

    var str = "";
    var len = text1.length;
    var whitespacelen = totallen - len;
    if (whitespacelen < 0) {
        str = text1.substring(0, totallen);
    } else {
        str = text1;
        for (var i = 1; i <= whitespacelen; i++) {
            str += " ";
        }

    }


    return str;
}
function spaces(noSpaces) {
    var str = "";
    for (var i = 1; i <= noSpaces; i++) {
        str += " ";

    }
    return str;

}
function printZPLImage() {
    var applet = document.jzebra;
    if (applet != null) {
        // Sample text
        applet.append("^XA\n");
        applet.append("^FO50,50^ADN,36,20^FDPRINTED USING JZEBRA " + applet.getVersion() + "\n");


        // *Note;  As of 2/14/2012, Raw image printing is only supported in
        // ZPLII and ESCP modes, and is an experimental feature.
        // 
        // A second parameter MUST be specified to "appendImage()", for 
        // jZebra to use raw image printing.  If this is not supplied, jZebra
        // will send PostScript data to your raw printer!  This is bad!
        //      - Make sure image width and image height are divisible by 8.
        //      - ESCP image widths should be EXACTLY the pixel width of the
        //           printer according to the ESCP printing guidelines
        //      - ESCP support uses the "ESC V" method.  If "ESC ." is needed
        //           contact the mailing list.
        //      - The applet will append the special raw markup:
        //           i.e. ^GFA, char(27), etc.
        // applet.appendImage("logo.png", "ESCP");

        applet.appendImage(getPath() + "img/image_sample_bw.png", "ZPLII");
        while (!applet.isDoneAppending()) {
            // Note, enless while loops are bad practice.
            // Create a JavaScript function called "jzebraDoneAppending()"
            // instead and handle your next steps there.
        }


        // Finish printing
        applet.append("^FS\n");
        applet.append("^XZ\n");

        // Send characters/raw commands to printer
        applet.print();
    }

    // *Note:  monitorPrinting() still works but is too complicated and
    // outdated.  Instead create a JavaScript  function called 
    // "jzebraDonePrinting()" and handle your next steps there.
    monitorPrinting();
}


function print64() {
    var applet = document.jzebra;
    if (applet != null) {
        // Use jZebra's `"append64"` function. This will automatically convert provided
        // base64 encoded text into ascii/bytes, etc.
        applet.append64("QTU5MCwxNjAwLDIsMywxLDEsTiwialplYnJhIHNhbXBsZS5odG1sIgpBNTkwLDE1NzAsMiwzLDEsMSxOLCJUZXN0aW5nIHRoZSBwcmludDY0KCkgZnVuY3Rpb24iClAxCg==");

        // Send characters/raw commands to printer
        applet.print();
    }

    // *Note:  monitorPrinting() still works but is too complicated and
    // outdated.  Instead create a JavaScript  function called 
    // "jzebraDonePrinting()" and handle your next steps there.
    monitorPrinting();
}

function printPages() {
    var applet = document.jzebra;
    if (applet != null) {
        applet.append("A590,1600,2,3,1,1,N,\"jZebra 1\"\n");
        applet.append("A590,1570,2,3,1,1,N,\"Testing the printPages() function\"\n");
        applet.append("P1\n");

        applet.append("A590,1600,2,3,1,1,N,\"jZebra 2\"\n");
        applet.append("A590,1570,2,3,1,1,N,\"Testing the printPages() function\"\n");
        applet.append("P1\n");

        applet.append("A590,1600,2,3,1,1,N,\"jZebra 3\"\n");
        applet.append("A590,1570,2,3,1,1,N,\"Testing the printPages() function\"\n");
        applet.append("P1\n");

        applet.append("A590,1600,2,3,1,1,N,\"jZebra 4\"\n");
        applet.append("A590,1570,2,3,1,1,N,\"Testing the printPages() function\"\n");
        applet.append("P1\n");

        applet.append("A590,1600,2,3,1,1,N,\"jZebra 5\"\n");
        applet.append("A590,1570,2,3,1,1,N,\"Testing the printPages() function\"\n");
        applet.append("P1\n");

        applet.append("A590,1600,2,3,1,1,N,\"jZebra 6\"\n");
        applet.append("A590,1570,2,3,1,1,N,\"Testing the printPages() function\"\n");
        applet.append("P1\n");

        applet.append("A590,1600,2,3,1,1,N,\"jZebra 7\"\n");
        applet.append("A590,1570,2,3,1,1,N,\"Testing the printPages() function\"\n");
        applet.append("P1\n");

        applet.append("A590,1600,2,3,1,1,N,\"jZebra 8\"\n");
        applet.append("A590,1570,2,3,1,1,N,\"Testing the printPages() function\"\n");
        applet.append("P1\n");

        // Mark the end of a label, in this case  P1 plus a newline character
        // jZebra knows to look for this and treat this as the end of a "page"
        // for better control of larger spooled jobs (i.e. 50+ labels)
        applet.setEndOfDocument("P1\n");

        // The amount of labels to spool to the printer at a time. When
        // jZebra counts this many `EndOfDocument`'s, a new print job will 
        // automatically be spooled to the printer and counting will start
        // over.
        applet.setDocumentsPerSpool("3");

        // Send characters/raw commands to printer
        applet.print();

    }

    // *Note:  monitorPrinting() still works but is too complicated and
    // outdated.  Instead create a JavaScript  function called 
    // "jzebraDonePrinting()" and handle your next steps there.
    monitorPrinting();
}

function printXML() {
    var applet = document.jzebra;
    if (applet != null) {
        // Appends the contents of an XML file from a SOAP response, etc.
        // a valid relative URL or a valid complete URL is required for the XML
        // file.  The second parameter must be a valid XML tag/node containing
        // base64 encoded data, i.e. <node_1>aGVsbG8gd29ybGQ=</node_1>
        // Example:
        //     applet.appendXML("http://yoursite.com/zpl.xml", "node_1");
        //     applet.appendXML("http://justtesting.biz/jZebra/dist/epl.xml", "v7:Image");
        applet.appendXML(getPath() + "misc/zpl_sample.xml", "v7:Image");

        // Send characters/raw commands to printer
        //applet.print(); // Can't do this yet because of timing issues with XML
    }

    // Monitor the append status of the xml file, prints when appending if finished
    // *Note:  monitorAppending() still works but is too complicated and
    // outdated.  Instead create a JavaScript  function called 
    // "jzebraDoneAppending()" and handle your next steps there.
    monitorAppending();
}

function printHex() {
    var applet = document.jzebra;
    if (applet != null) {
        // Using jZebra's "append()" function, hexadecimanl data can be sent
        // by using JavaScript's "\x00" notation. i.e. "41 35 39 30 2c ...", etc
        // Example: 
        //     applet.append("\x41\x35\x39\x30\x2c"); // ...etc
        applet.append("\x41\x35\x39\x30\x2c\x31\x36\x30\x30\x2c\x32\x2c\x33\x2c\x31\x2c\x31\x2c\x4e\x2c\x22\x6a\x5a\x65\x62\x72\x61\x20\x73\x61\x6d\x70\x6c\x65\x2e\x68\x74\x6d\x6c\x22\x0A\x41\x35\x39\x30\x2c\x31\x35\x37\x30\x2c\x32\x2c\x33\x2c\x31\x2c\x31\x2c\x4e\x2c\x22\x54\x65\x73\x74\x69\x6e\x67\x20\x74\x68\x65\x20\x70\x72\x69\x6e\x74\x48\x65\x78\x28\x29\x20\x66\x75\x6e\x63\x74\x69\x6f\x6e\x22\x0A\x50\x31\x0A");

        // Send characters/raw commands to printer
        applet.print();


    }

    // *Note:  monitorPrinting() still works but is too complicated and
    // outdated.  Instead create a JavaScript  function called 
    // "jzebraDonePrinting()" and handle your next steps there.
    monitorPrinting();

    /**
    *  CHR/ASCII PRINTING:
    *  // Appends CHR(27) + CHR(29) using `"fromCharCode"` function
    *  // CHR(27) is commonly called the "ESCAPE" character
    *  document.jzebra.append(String.fromCharCode(27) + String.fromCharCode(29));
    */
}


function printFile() {
    var applet = document.jzebra;
    if (applet != null) {
        // Using jzebra's "appendFile()" function, a file containg your raw EPL/ZPL
        // can be sent directly to the printer
        // Example: 
        //     applet.appendFile("http://yoursite/zpllabel.txt"); // ...etc
        applet.appendFile(getPath() + "misc/zpl_sample.txt");
        applet.print();
    }

    // *Note:  monitorPrinting() still works but is too complicated and
    // outdated.  Instead create a JavaScript  function called 
    // "jzebraDonePrinting()" and handle your next steps there.
    monitorPrinting();
}


function printImage(NoOfPrint) {
    var applet = document.jzebra;
    if (applet != null) {
        // Using jZebra's "appendImage()" function, a png, jpeg file
        // can be sent directly to the printer supressing the print dialog
        // Example:
        //     applet.appendImage("http://yoursite/logo1.png"); // ...etc

        // Sample only: Searches for locally installed printer with "pdf" in the name
        // Can't use Zebra, because this function needs a PostScript capable printer
        applet.findPrinter("\\{dummy printer name for listing\\}");
        while (!applet.isDoneFinding()) {
            // Note, enless while loops are bad practice.
            // Create a JavaScript function called "jzebraDoneFinding()"
            // instead and handle your next steps there.
        }

        // Sample only: If a PDF printer isn't installed, try the Microsoft XPS Document
        // Writer.  Replace this with your printer name.
        //        var breakpoint = 0;
        //        var printers = applet.getPrinters().split(",");
        //        for (i in printers) {
        //            if (breakpoint == 0) {
        //                if (printers[i].indexOf("Brother QL-570") != -1) {
        //                    applet.setPrinter(i);
        //                    breakpoint = 1;

        //                }
        //            }
        //        }

        //        // No suitable printer found, exit
        //        if (applet.getPrinter() == null) {
        //            alert("Could not find a suitable printer for printing an image.");
        //            return;
        //        }
        useDefaultPrinter();
        var image = PrintObjectElement;
        for (i = 0; i < NoOfPrint; i++) {

            document.jzebra.appendImage(image);
            document.jzebra.append("\n");
            monitorAppending2();


        };

    }

    // Very important for images, uses printPS() insetad of print()
    // *Note:  monitorAppending2() still works but is too complicated and
    // outdated.  Instead create a JavaScript  function called 
    // "jzebraDoneAppending()" and handle your next steps there.

}
function Customprint(url) {
    var applet = document.jzebra;
    if (applet != null) {
        // Using jZebra's "appendImage()" function, a png, jpeg file
        // can be sent directly to the printer supressing the print dialog
        // Example:
        //     applet.appendImage("http://yoursite/logo1.png"); // ...etc

        // Sample only: Searches for locally installed printer with "pdf" in the name
        // Can't use Zebra, because this function needs a PostScript capable printer
        applet.findPrinter("\\{dummy printer name for listing\\}");
        while (!applet.isDoneFinding()) {
            // Note, enless while loops are bad practice.
            // Create a JavaScript function called "jzebraDoneFinding()"
            // instead and handle your next steps there.
        }

        // Sample only: If a PDF printer isn't installed, try the Microsoft XPS Document
        // Writer.  Replace this with your printer name.
        //        var breakpoint = 0;
        //        var printers = applet.getPrinters().split(",");
        //        for (i in printers) {
        //            if (breakpoint == 0) {
        //                if (printers[i].indexOf("Brother QL-570") != -1) {
        //                    applet.setPrinter(i);
        //                    breakpoint = 1;

        //                }
        //            }
        //        }

        //        // No suitable printer found, exit
        //        if (applet.getPrinter() == null) {
        //            alert("Could not find a suitable printer for printing an image.");
        //            return;
        //        }
        useDefaultPrinter();
        //var image = PrintObjectElement;


        document.jzebra.appendImage(url);
        document.jzebra.append("\n");
        monitorAppending2();


        // };

    }

    // Very important for images, uses printPS() insetad of print()
    // *Note:  monitorAppending2() still works but is too complicated and
    // outdated.  Instead create a JavaScript  function called 
    // "jzebraDoneAppending()" and handle your next steps there.

}
function monitorAppending2() {
    var applet = document.jzebra;
    if (applet != null) {
        if (!applet.isDoneAppending()) {
            monitorAppending2();


        } else {
            applet.printPS(); // Don't print until all of the image data has been appended

            // *Note:  monitorPrinting() still works but is too complicated and
            // outdated.  Instead create a JavaScript  function called 
            // "jzebraDonePrinting()" and handle your next steps there.
            monitorPrinting();
        }
    } else {
        alert("Applet not loaded!");
    }
}

function printPDF() {
    var applet = document.jzebra;
    if (applet != null) {
        applet.findPrinter("\\{dummy printer name for listing\\}");
        while (!applet.isDoneFinding()) {
            // Note, enless while loops are bad practice.
            // Create a JavaScript function called "jzebraDoneFinding()"
            // instead and handle your next steps there.
        }

        // Sample only: If a PDF printer isn't installed, try the Microsoft XPS Document
        // Writer.  Replace this with your printer name.
        var printers = applet.getPrinters().split(",");
        for (i in printers) {
            if (printers[i].indexOf("Microsoft XPS") != -1 ||
			printers[i].indexOf("PDF") != -1) {
                applet.setPrinter(i);
            }
        }

        // No suitable printer found, exit
        if (applet.getPrinter() == null) {
            alert("Could not find a suitable printer for a PDF document");
            return;
        }

        // Append our pdf (only one pdf can be appended per print)
        applet.appendPDF(getPath() + "misc/pdf_sample.pdf");
    }

    // Very important for PDF, uses printPS() instead of print()
    // *Note:  monitorAppending2() still works but is too complicated and
    // outdated.  Instead create a JavaScript  function called 
    // "jzebraDoneAppending()" and handle your next steps there.
    monitorAppending2();
}

// Gets the current url's path, such as http://site.com/example/dist/
function getPath() {
    var path = window.location.href;
    return path.substring(0, path.lastIndexOf("/")) + "/";
}



function printHTML() {
    var applet = document.jzebra;
    if (applet != null) {
        applet.findPrinter("\\{dummy printer name for listing\\}");
        while (!applet.isDoneFinding()) {
            // Wait
        }

        // Sample only: If a PDF printer isn't installed, try the Microsoft XPS Document
        // Writer.  Replace this with your printer name.
        var c = 0;
        var printers = applet.getPrinters().split(",");
        for (i in printers) {
            if (c == 0) {
                if (printers[i].indexOf(printname) != -1 ||
                    printers[i].indexOf("PDF") != -1) {
                    applet.setPrinter(i);
                    c = 1;
                }
            }
        }

        // No suitable printer found, exit
        if (applet.getPrinter() == null) {
            alert("Could not find a suitable printer for an HTML document");
            return;
        }

        // Preserve formatting for white spaces, etc.
        //        var colA = fixHTML('<table  border=\'1\' style="font-size:10pt;border-collapse=\'collapse\'; font-face:\'Courier New\';"><tr><td>' + 213564 + '</td></tr><tr><td>' + 213564 + '</td></tr><tr><td>' + 213564 + '</td></tr><tr><td>' + 213564 + '</td></tr><tr><td>' + 213564 + '</td></tr><tr><td>' + 213564 + '</td></tr><tr><td>' + 213564 + '</td></tr><tr><td>' + 213564 + '</td></tr><tr><td>' + 213564 + '</td></tr><tr><td>' + 213564 + '</td></tr><tr><td>' + 213564 + '</td></tr><tr><td>' + 213564 + '</td></tr><tr><td>' + 213564 + '</td></tr><tr><td>' + 213564 + '</td></tr><tr><td>' + 213564 + '</td></tr><tr><td>' + 213564 + '</td></tr><tr><td>' + 213564 + '</td></tr><tr><td>' + 213564 + '</td></tr><tr><td>' + 213564 + '</td></tr><tr><td>' + 213564 + '</td></tr><tr><td>' + 213564 + '</td></tr><tr><td>' + 213564 + '</td></tr><tr><td>' + 213564 + '</td></tr><tr><td>' + 213564 + '</td></tr><tr><td>' + 213564 + '</td></tr></table>');
        ////        colA = colA + '<color=red>Version:</color> ' + applet.getVersion() + '<br />';
        ////        colA = colA + '<color=red>Visit:</color> http://code.google.com/p/jzebra';

        //        // HTML image
        //        var colB = '<img src="' + getPath() + 'img/image_sample.png">';

        // Append our image (only one image can be appended per print)
        // HTML



        applet.appendHTML('<html>' + PrintObjectElement + "</html>");


        // document.jzebra.setPaperSize("110mm", "97mm");  // A4
        //document.jzebra.setPaperSize("8.5in", "11.0in");  // US Letter
        //  document.jzebra.setAutoSize(true);               // Preserve proportions
        // document.jzebra.setOrientation("landscape");     // Default is "portrait"

    }
    // Printing an existing HTML file
    //   document.jzebra.appendHTMLFile("../form.html");

    // Wait for append to finish
    //  while (!document.jzebra.isDoneAppending()) { // wait }
    // Very important for html, uses printHTML() instead of print()
    // *Note:  monitorAppending3() still works but is too complicated and
    // outdated.  Instead create a JavaScript  function called 
    // "jzebraDoneAppending()" and handle your next steps there.
    monitorAppending3();
}

// Fixes some html formatting for printing. Only use on text, not on tags!  Very important!
//    1.  HTML ignores white spaces, this fixes that
//    2.  The right quotation mark breaks PostScript print formatting
//    3.  The hyphen/dash autoflows and breaks formatting  
function fixHTML(html) { return html.replace(/ /g, "&nbsp;").replace(/’/g, "'").replace(/-/g, "&#8209;"); }

function printToFile() {
    var applet = document.jzebra;
    if (applet != null) {
        // Send characters/raw commands to applet using "append"
        // Hint:  Carriage Return = \r, New Line = \n, Escape Double Quotes= \"
        applet.append("A590,1600,2,3,1,1,N,\"jZebra " + applet.getVersion() + " sample.html\"\n");
        applet.append("A590,1570,2,3,1,1,N,\"Testing the print() function\"\n");
        applet.append("P1\n");

        // Send characters/raw commands to file
        // Ex:  applet.printToFile("\\\\server\\printer");
        // Ex:  applet.printToFile("/home/user/test.txt");
        applet.printToFile("C:\\jzebra_test.txt");
    }

    // *Note:  monitorPrinting() still works but is too complicated and
    // outdated.  Instead create a JavaScript  function called 
    // "jzebraDonePrinting()" and handle your next steps there.
    monitorPrinting();
}

function chr(i) {
    return String.fromCharCode(i);
}

// *Note:  monitorPrinting() still works but is too complicated and
// outdated.  Instead create a JavaScript  function called 
// "jzebraDonePrinting()" and handle your next steps there.
function monitorPrinting() {
    var applet = document.jzebra;
    if (applet != null) {
        if (!applet.isDonePrinting()) {
            window.setTimeout('monitorPrinting()', 100);
        } else {
            var e = applet.getException();
            alert(e == null ? "Printed Successfully" : "Exception occured: " + e.getLocalizedMessage());
        }
    } else {
        alert("Applet not loaded!");
    }
}

function monitorFinding() {
    var applet = document.jzebra;
    if (applet != null) {
        if (!applet.isDoneFinding()) {
            window.setTimeout('monitorFinding()', 100);
        } else {
            var printer = applet.getPrinter();
            alert(printer == null ? "Printer not found" : "Printer \"" + printer + "\" found");
        }
    } else {
        alert("Applet not loaded!");
    }
}

function monitorFinding2() {
    var applet = document.jzebra;
    if (applet != null) {
        if (!applet.isDoneFinding()) {
            window.setTimeout('monitorFinding2()', 100);
        } else {
            var printersCSV = applet.getPrinters();
            var printers = printersCSV.split(",");
            for (p in printers) {
                alert(printers[p]);
            }

        }
    } else {
        alert("Applet not loaded!");
    }
}

// *Note:  monitorAppending() still works but is too complicated and
// outdated.  Instead create a JavaScript  function called 
// "jzebraDoneAppending()" and handle your next steps there.
function monitorAppending() {
    var applet = document.jzebra;
    if (applet != null) {
        if (!applet.isDoneAppending()) {
            window.setTimeout('monitorAppending()', 100);
        } else {
            applet.print(); // Don't print until all of the data has been appended

            // *Note:  monitorPrinting() still works but is too complicated and
            // outdated.  Instead create a JavaScript  function called 
            // "jzebraDonePrinting()" and handle your next steps there.
            monitorPrinting();
        }
    } else {
        alert("Applet not loaded!");
    }
}

// *Note:  monitorAppending2() still works but is too complicated and
// outdated.  Instead create a JavaScript  function called 
// "jzebraDoneAppending()" and handle your next steps there.
function monitorAppending2() {
    var applet = document.jzebra;
    if (applet != null) {
        if (!applet.isDoneAppending()) {
            window.setTimeout('monitorAppending2()', 100);
        } else {
            applet.printPS(); // Don't print until all of the image data has been appended

            // *Note:  monitorPrinting() still works but is too complicated and
            // outdated.  Instead create a JavaScript  function called 
            // "jzebraDonePrinting()" and handle your next steps there.
            monitorPrinting();
        }
    } else {
        alert("Applet not loaded!");
    }
}

// *Note:  monitorAppending3() still works but is too complicated and
// outdated.  Instead create a JavaScript  function called 
// "jzebraDoneAppending()" and handle your next steps there.
function monitorAppending3() {
    var applet = document.jzebra;
    if (applet != null) {
        if (!applet.isDoneAppending()) {
            window.setTimeout('monitorAppending3()', 100);
        } else {
            applet.printHTML(); // Don't print until all of the image data has been appended


            // *Note:  monitorPrinting() still works but is too complicated and
            // outdated.  Instead create a JavaScript  function called 
            // "jzebraDonePrinting()" and handle your next steps there.
            monitorPrinting();
        }
    } else {
        alert("Applet not loaded!");
    }
}

function useDefaultPrinter() {
    var applet = document.jzebra;
    if (applet != null) {
        // Searches for default printer
        applet.findPrinter();
    }

    monitorFinding();
}

function jzebraReady() {
    // Change title to reflect version
    var applet = document.jzebra;

    //Chenge By Khem below code is only for notify that applet is ready or not. 
    /* var title = document.getElementById("title");
    if (applet != null) {

    title.innerHTML = title.innerHTML + " " + applet.getVersion();
    document.getElementById("content").style.background = "#F0F0F0";
    }*/
}

/**
* By default, jZebra prevents multiple instances of the applet's main 
* JavaScript listener thread to start up.  This can cause problems if
* you have jZebra loaded on multiple pages at once. 
* 
* The downside to this is Internet Explorer has a tendency to initilize the
* applet multiple times, so use this setting with care.
*/
function allowMultiple() {
    var applet = document.jzebra;
    if (applet != null) {
        var multiple = applet.getAllowMultipleInstances();
        applet.allowMultipleInstances(!multiple);
        alert('Allowing of multiple applet instances set to "' + !multiple + '"');
    }
}

function printPage() {
    $("#content").html2canvas({
        canvas: hidden_screenshot,
        onrendered: function () { printBase64Image($("canvas")[0].toDataURL('image/png')); }
    });
}

function printBase64Image(base64data) {
    var applet = document.jzebra;
    if (applet != null) {
        applet.findPrinter("\\{dummy printer name for listing\\}");
        while (!applet.isDoneFinding()) {
            // Note, endless while loops are bad practice.
        }

        var printers = applet.getPrinters().split(",");
        for (i in printers) {
            if (printers[i].indexOf("Microsoft XPS") != -1 ||
			printers[i].indexOf("PDF") != -1) {
                applet.setPrinter(i);
            }
        }

        // No suitable printer found, exit
        if (applet.getPrinter() == null) {
            alert("Could not find a suitable printer for printing an image.");
            return;
        }

        // Optional, set up custom page size.  These only work for PostScript printing.
        // setPaperSize() must be called before setAutoSize(), setOrientation(), etc.
        applet.setPaperSize("8.5in", "11.0in");  // US Letter
        applet.setAutoSize(true);
        applet.appendImage(base64data);
    }

    // Very important for images, uses printPS() insetad of print()
    // *Note:  monitorAppending2() still works but is too complicated and
    // outdated.  Instead create a JavaScript  function called 
    // "jzebraDoneAppending()" and handle your next steps there.
    monitorAppending2();
}

function logFeatures() {
    if (document.jzebra != null) {
        var applet = document.jzebra;
        var logging = applet.getLogPostScriptFeatures();
        applet.setLogPostScriptFeatures(!logging);
        alert('Logging of PostScript printer capabilities to console set to "' + !logging + '"');
    }
}