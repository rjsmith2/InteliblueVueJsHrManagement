
export default interface EmployeeDetail {
    firstName: string,
    lastName: string,
    email: string,
    phone: string,
    department: { id: number, name: string }
}