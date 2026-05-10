export class ResetHelper {
  constructor(private request: any) {}

  async reset() {
    await this.request.post('http://localhost:5000/api/test/reset')
  }
}