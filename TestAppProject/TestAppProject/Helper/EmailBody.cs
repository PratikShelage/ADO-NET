namespace TestAppProject.Helper
{
    public class EmailBody
    {
        public static string EmailStringBody(string email, string emailToken)
        {
            return $@"<html>
<head>
</head>
<body style=""margin: 0; padding: 0; font-family: Arial, Helvetica, sans-serif;"">
<div style=""height: auto; background: linear-gradient(to top,#c9c9ff 50%,#6e6efe 90%) no-repeat;width: 400px; padding: 30px;"">
  <div>
    <div>
      <h1>Reset Your Password</h1>
      <hr>
      <p>You're receiving this e-mail because you requested a password reset for your myApp Account.</p>
      <p>Please tap the button bellow to choose a new password</p>

      <a href=""http://localhost:4200/reset?email={email}&code={emailToken}"" target=""_blank"">Reset Password</a>
      <p>Kind Request,<br><br>
      MyApp</p>
    </div>
  </div>

</div>
</body>
</html>
";
        }
    }
}
