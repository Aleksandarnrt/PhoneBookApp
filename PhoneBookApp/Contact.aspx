<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Contact.aspx.cs" Inherits="PhoneBookApp.Contact" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Phonebook App</title>
    <link rel = "stylesheet" type = "text/css" href = "assets/css/style.css" /> 
    
</head>
<body> 
    <header>
        <h1 id="phoneBookHeader">Phonebook</h1>
        <div class="formWrapper">
            <form id="form1" runat="server">
                <div class="inputWrapper">
                    <asp:HiddenField ID="hfContactID" runat="server" />
                    <asp:Label ID="firstname" CssClass="lbl" runat ="server" Text="Firstname"></asp:Label>
                    <asp:TextBox ID="txtFirstame" runat="server" ></asp:TextBox>
                    
                    <asp:Label ID="lastname" CssClass="lbl" runat="server" Text="Lastname"></asp:Label>
                    <asp:TextBox ID="txtLastname" runat="server"></asp:TextBox>
                
                    <asp:Label ID="phone" CssClass="lbl" runat="server" Text="Phone Number"></asp:Label>
                    <asp:TextBox ID="txtPhoneNumber" runat="server"></asp:TextBox>
                </div>
                <asp:Label ID="message" CssClass="error-label" runat="server"></asp:Label>
                <div class="btnWrapper"> 
                    <asp:Button ID="btnAdd"  CssClass="btnVisual" runat="server" Text="Add Contact" onClick="btnAdd_Click" />
                    <asp:Button ID="btnClear" CssClass="btnVisual" runat="server" Text="Clear" OnClick="btnClear_Click" />
                    <asp:Button ID="btnDelete" CssClass="btnVisual" runat="server" Text="Delete" OnClick="btnDelete_Click" />
                </div>
                <div class="form-search">
                  <input type="text" id="search" class="search" onkeyup="searchFunction()" placeholder="What you looking for..." />
                </div>
              <asp:GridView ID="gvContact" runat="server" AutoGenerateColumns="False">
                  <Columns>
                      <asp:BoundField DataField="ContactFirstname" HeaderText="Firstname" />
                      <asp:BoundField DataField="ContactLastName" HeaderText="Lastname" />
                      <asp:BoundField DataField="phoneNumber" HeaderText="Phone Number" />
                      <asp:TemplateField HeaderText="Action">
                          <ItemTemplate>
                              <asp:LinkButton ID="lnkView" CssClass="edit" runat="server" CommandArgument='<%# Eval("ContactID") %>' OnClick="lnkView_OnClick">Edit Contact</asp:LinkButton>
                          </ItemTemplate>
                      </asp:TemplateField>
                  </Columns>
              </asp:GridView>
            </form>
            <%--<div class="pagination">
              <a id="previous-page" href ="#">&laquo;</a>
            </div>--%>
        </div> 
        
    </header>
    <script src="assets/js/jquery-3.3.1.min.js"></script>
   <!-- <script src="assets/js/tether.js"></script>-->
    <script src="assets/js/bootstrap.min.js"></script>
    <script src="assets/js/search.js"></script>
    <%--<script src="assets/js/script.js"></script>--%>
    
</body>
</html>
