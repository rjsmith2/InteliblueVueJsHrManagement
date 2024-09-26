<script lang="ts">
	import { defineComponent, reactive } from "vue";
	import emitter from '@/contexts/emitter';
	import EmployeeService from "@/services/EmployeeService"
	import type ResponseData from "@/types/ResponseData";
	import type ResponseErrorData from "@/types/ResponseErrorData";
	import type EmployeeDetail from "@/types/EmployeeDetail";

	export default defineComponent({
		name: 'employee-list',
		data() {
			return {
				loading: false,
				errorMessage: "",
				employees: [] as EmployeeDetail
			};
		},

		methods: {
			fetchEmployees() {
				this.loading = true;
				return EmployeeService.getAll()
					.then((response: ResponseData) => {
						this.employees = response.data as EmployeeDetail[];
					})
					.finally(() => {
						this.loading = false;
					});
			},
			editEmployee(id: number) {
				emitter.emit('openEmployee', { id });
			},
			deleteEmployee(id: number) {
				EmployeeService.delete(id).then((response: ResponseData) => {
					this.fetchEmployees();
				}).
					catch((e) => {
						this.errorMessage = e.error?.message || e.message || e.title;
					});
			},
			employeeAddedEventHandler() {
				this.fetchEmployees();
			},
			employeeEdittedEventHandler() {
				this.fetchEmployees();
			}
		},
		created() {
			emitter.on('employeeAdded', this.employeeAddedEventHandler);
			emitter.on('employeeEdited', this.employeeEdittedEventHandler);
			this.fetchEmployees();
		},
		destroyed() {
			emitter.off('employeeAdded', this.employeeAddedEventHandler);
			emitter.off('employeeEdited', this.employeeEdittedEventHandler);
		}
	});
</script>

<template>
	<div v-if="errorMessage" class="bg-red-100 border border-red-400 text-red-700 px-4 py-3 rounded relative m-3" role="alert">
		<span class="block sm:inline">{{errorMessage}}</span>
	</div>
	<div class="flex flex-col w-full h-full overflow-scroll text-gray-700 bg-white shadow-md rounded-xl bg-clip-border">

		<table class="w-full text-left table-auto min-w-max">
			<thead>
				<tr>
					<th class="p-4 border-b border-blue-gray-100 bg-blue-gray-50">
						<p class="block font-sans text-sm antialiased font-normal leading-none text-blue-gray-900 opacity-70">
							Name
						</p>
					</th>
					<th class="p-4 border-b border-blue-gray-100 bg-blue-gray-50">
						<p class="block font-sans text-sm antialiased font-normal leading-none text-blue-gray-900 opacity-70">
							Email
						</p>
					</th>
					<th class="p-4 border-b border-blue-gray-100 bg-blue-gray-50">
						<p class="block font-sans text-sm antialiased font-normal leading-none text-blue-gray-900 opacity-70">
							Phone
						</p>
					</th>
					<th class="p-4 border-b border-blue-gray-100 bg-blue-gray-50">
						<p class="block font-sans text-sm antialiased font-normal leading-none text-blue-gray-900 opacity-70">
							Email
						</p>
					</th>
					<th class="p-4 border-b border-blue-gray-100 bg-blue-gray-50">
						<p class="block font-sans text-sm antialiased font-normal leading-none text-blue-gray-900 opacity-70">
							Department
						</p>
					</th>
					<th class="p-4 border-b border-blue-gray-100 bg-blue-gray-50">
						<p class="block font-sans text-sm antialiased font-normal leading-none text-blue-gray-900 opacity-70"></p>
					</th>
				</tr>
			</thead>
			<tbody>
				<tr v-for="employee in employees">
					<td class="p-4 border-b border-blue-gray-50">
						<p class="block font-sans text-sm antialiased font-normal leading-normal text-blue-gray-900">
							{{[employee.firstName, employee.lastName].join(" ")}}
						</p>
					</td>
					<td class="p-4 border-b border-blue-gray-50">
						<p class="block font-sans text-sm antialiased font-normal leading-normal text-blue-gray-900">
							{{employee.email}}
						</p>
					</td>
					<td class="p-4 border-b border-blue-gray-50">
						<p class="block font-sans text-sm antialiased font-normal leading-normal text-blue-gray-900">
							{{employee.phone}}
						</p>
					</td>
					<td class="p-4 border-b border-blue-gray-50">
						<p class="block font-sans text-sm antialiased font-normal leading-normal text-blue-gray-900">
							{{employee.department.name}}
						</p>
					</td>
					<td class="p-4 border-b border-blue-gray-50">
						<button @click="editEmployee(employee.id)" class="font-sans text-sm antialiased font-medium leading-normal text-green-500 p-2">
							<svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-pen" viewBox="0 0 16 16">
								<path d="m13.498.795.149-.149a1.207 1.207 0 1 1 1.707 1.708l-.149.148a1.5 1.5 0 0 1-.059 2.059L4.854 14.854a.5.5 0 0 1-.233.131l-4 1a.5.5 0 0 1-.606-.606l1-4a.5.5 0 0 1 .131-.232l9.642-9.642a.5.5 0 0 0-.642.056L6.854 4.854a.5.5 0 1 1-.708-.708L9.44.854A1.5 1.5 0 0 1 11.5.796a1.5 1.5 0 0 1 1.998-.001m-.644.766a.5.5 0 0 0-.707 0L1.95 11.756l-.764 3.057 3.057-.764L14.44 3.854a.5.5 0 0 0 0-.708z"></path>
							</svg>
						</button>
						<button @click="deleteEmployee(employee.id)" class="font-sans text-sm antialiased font-medium leading-normal text-red-500 p-2" :disabled="loading">
							<svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-trash" viewBox="0 0 16 16">
								<path d="M5.5 5.5A.5.5 0 0 1 6 6v6a.5.5 0 0 1-1 0V6a.5.5 0 0 1 .5-.5m2.5 0a.5.5 0 0 1 .5.5v6a.5.5 0 0 1-1 0V6a.5.5 0 0 1 .5-.5m3 .5a.5.5 0 0 0-1 0v6a.5.5 0 0 0 1 0z"></path>
								<path d="M14.5 3a1 1 0 0 1-1 1H13v9a2 2 0 0 1-2 2H5a2 2 0 0 1-2-2V4h-.5a1 1 0 0 1-1-1V2a1 1 0 0 1 1-1H6a1 1 0 0 1 1-1h2a1 1 0 0 1 1 1h3.5a1 1 0 0 1 1 1zM4.118 4 4 4.059V13a1 1 0 0 0 1 1h6a1 1 0 0 0 1-1V4.059L11.882 4zM2.5 3h11V2h-11z"></path>
							</svg>
						</button>
					</td>
				</tr>
			</tbody>
		</table>
	</div>
</template>
