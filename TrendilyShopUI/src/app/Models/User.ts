export class User {
  email: string;
  userName: string;
  token: string;
  role: string;

  constructor(Email: string, UserName: string, Token: string, Role: string) {
    this.email = Email;
    this.userName = UserName;
    this.token = Token;
    this.role = Role;
  }
}
