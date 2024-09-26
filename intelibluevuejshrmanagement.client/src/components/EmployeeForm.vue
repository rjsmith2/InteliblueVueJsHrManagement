<script lang="ts">
	import { defineComponent, reactive } from "vue";
	import emitter from '@/contexts/emitter';
	import EmployeeService from "@/services/EmployeeService"
	import DepartmentService from "@/services/DepartmentService"
	import type ResponseData from "@/types/ResponseData";
	import type ResponseErrorData from "@/types/ResponseErrorData";
	import type Department from "@/types/Department";


	export default defineComponent({
		name: 'employee-form',
		data() {
			return {
				loading: false,
				successMessage: "",
				errorMessage: "",
				departments: [] as Department[],
				form: this.newFormData(),
				errorForm: {

				}
			};
		},

		methods: {
			newFormData() {
				return {
					firstName: "",
					lastName: "",
					department: null,
					email: "",
					phone: ""

				}
			},
			newEmployee() {
				this.employeeId = null;
				this.form = this.newFormData();
				this.setDefaultDepartment();
				this.clearMessages();
			},
			clearMessages() {
				this.errorMessage = "";
				this.successMessage = "";
				this.errorForm = {};
			},
			loadEmployee(id: number) {
				this.loading = true;
				this.clearMessages();
				return EmployeeService.get(id)
					.then((response: ResponseData) => {
						this.form = {
							...this.newFormData(),
							firstName: response.data.firstName,
							lastName: response.data.lastName,
							phone: response.data.phone,
							email: response.data.email,
							department: response.data.department.id
						};;
						this.employeeId = response.data.id;

					})
					.catch((e: ResponseErrorData) => {
						this.errorForm = e.errors || {};
						this.errorMessage = e.message || e.title;
					})
					.finally(() => {
						this.loading = false;
					});
			},
			setDefaultDepartment() {
				this.form.department = this.form.department || this.departments[0].id;
			},
			fetchDepartments() {
				return DepartmentService.getAll()
					.then((response: ResponseData) => {
						this.departments = response.data as Department[];
						this.setDefaultDepartment();
					});
			},
			handleSubmit() {
				this.clearMessages();
				const data = {
					...this.form
				};
				this.loading = true;
				const submitAction = this.employeeId == null
					? EmployeeService.create(this.form).then((response: ResponseData) => {
						this.newEmployee();
						this.successMessage = "Successfully added!";
						this.form = this.newFormData();
						emitter.emit('employeeAdded');

					})
					: EmployeeService.update(this.employeeId, this.form).then((response: ResponseData) => {
						this.newEmployee();
						this.successMessage = "Successfully updated!";
						this.form = this.newFormData();
						emitter.emit('employeeEdited', { id: this.employeeId });

					});

				submitAction
					.catch((e: ResponseErrorData) => {
						this.errorForm = e.errors || {};
						this.errorMessage = e.message || e.title;
					})
					.finally(() => {
						this.loading = false;
					});
			},
			openEmployeeEventHandler({ id }: { id: number }) {
				this.loadEmployee(id);
			}
		},
		async created() {
			emitter.on('openEmployee', this.openEmployeeEventHandler)
			this.loading = true;
			await this.fetchDepartments();
			this.loading = false;
		},
		destroyed() {
			emitter.off('openEmployee', this.openEmployeeEventHandler)
		}

	});
</script>

<template>
	<div v-if="errorMessage" class="bg-red-100 border border-red-400 text-red-700 px-4 py-3 rounded relative" role="alert">
		<span class="block sm:inline">{{errorMessage}}</span>
	</div>
	<div v-if="successMessage" class="flex items-center bg-blue-500 text-white text-sm font-bold px-4 py-3" role="alert">
		<svg class="fill-current w-4 h-4 mr-2" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 20 20"><path d="M12.432 0c1.34 0 2.01.912 2.01 1.957 0 1.305-1.164 2.512-2.679 2.512-1.269 0-2.009-.75-1.974-1.99C9.789 1.436 10.67 0 12.432 0zM8.309 20c-1.058 0-1.833-.652-1.093-3.524l1.214-5.092c.211-.814.246-1.141 0-1.141-.317 0-1.689.562-2.502 1.117l-.528-.88c2.572-2.186 5.531-3.467 6.801-3.467 1.057 0 1.233 1.273.705 3.23l-1.391 5.352c-.246.945-.141 1.271.106 1.271.317 0 1.357-.392 2.379-1.207l.6.814C12.098 19.02 9.365 20 8.309 20z" /></svg>
		<p>{{successMessage}}</p>
	</div>
	<form @submit.prevent="handleSubmit" class="w-full my-2" novalidate="novalidate" :inert="loading" autocomplete="off">
		<div class="flex flex-wrap -mx-3 mb-6">
			<div class="w-full md:w-1/2 px-3 mb-6 md:mb-0">
				<label class="block uppercase tracking-wide text-gray-700 text-xs font-bold mb-2" for="grid-first-name">
					First Name
				</label>
				<input v-model="form.firstName" class="appearance-none block w-full bg-gray-200 text-gray-700 border rounded py-3 px-4 leading-tight focus:outline-none focus:bg-white" id="grid-first-name" type="text" placeholder="First Name">
				<p v-if="errorForm.firstName" class="text-red-500 text-xs italic"> {{ errorForm.firstName.join(",")}}</p>
			</div>
			<div class="w-full md:w-1/2 px-3">
				<label class="block uppercase tracking-wide text-gray-700 text-xs font-bold mb-2" for="grid-last-name">
					Last Name
				</label>
				<input v-model="form.lastName" class="appearance-none block w-full bg-gray-200 text-gray-700 border border-gray-200 rounded py-3 px-4 leading-tight focus:outline-none focus:bg-white focus:border-gray-500" id="grid-last-name" type="text" placeholder="Last Name">
				<p v-if="errorForm.lastName" class="text-red-500 text-xs italic"> {{ errorForm.lastName.join(",")}}</p>
			</div>
		</div>
		<div class="flex flex-wrap -mx-3 mb-6">
			<div class="w-full md:w-1/2 px-3 mb-6 md:mb-0">
				<label class="block uppercase tracking-wide text-gray-700 text-xs font-bold mb-2" for="grid-first-name">
					Email
				</label>
				<input v-model="form.email" class="appearance-none block w-full bg-gray-200 text-gray-700 border rounded py-3 px-4 leading-tight focus:outline-none focus:bg-white" id="grid-email" type="email" placeholder="Email">
				<p v-if="errorForm.email" class="text-red-500 text-xs italic"> {{ errorForm.email.join(",")}}</p>
			</div>
			<div class="w-full md:w-1/2 px-3">
				<label class="block uppercase tracking-wide text-gray-700 text-xs font-bold mb-2" for="grid-last-name">
					Phone
				</label>
				<input v-model="form.phone" class="appearance-none block w-full bg-gray-200 text-gray-700 border border-gray-200 rounded py-3 px-4 leading-tight focus:outline-none focus:bg-white focus:border-gray-500" id="grid-phone" type="text" placeholder="Phone Number">
				<p v-if="errorForm.phone" class="text-red-500 text-xs italic"> {{ errorForm.phone.join(",")}}</p>
			</div>
		</div>
		<div class="flex flex-wrap -mx-3 mb-2">
			<div class="w-full px-3 mb-6">
				<label class="block uppercase tracking-wide text-gray-700 text-xs font-bold mb-2" for="grid-department">
					Department
				</label>
				<div class="relative">
					<select v-model="form.department" class="block appearance-none w-full bg-gray-200 border border-gray-200 text-gray-700 py-3 px-4 pr-8 rounded leading-tight focus:outline-none focus:bg-white focus:border-gray-500" id="grid-department">
						<option v-for="department in departments"
								:value="department.id">
							{{department.name}}
						</option>
					</select>
					<div class="pointer-events-none absolute inset-y-0 right-0 flex items-center px-2 text-gray-700">
						<svg class="fill-current h-4 w-4" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 20 20"><path d="M9.293 12.95l.707.707L15.657 8l-1.414-1.414L10 10.828 5.757 6.586 4.343 8z" /></svg>
					</div>
				</div>
				<p v-if="errorForm.department" class="text-red-500 text-xs italic"> {{ errorForm.department.join(",")}}</p>
			</div>
		</div>
		<div class="flex items-center justify-between">
			<button class="border border-blue-500 mx-2 hover:bg-blue-700 text-blue-500 font-bold py-2 px-4 rounded focus:outline-none focus:shadow-outline" type="button" @click="newEmployee()" :disabled="loading">
				Reset
			</button>
			<button class="bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded focus:outline-none focus:shadow-outline" type="submit" :disabled="loading">
				Save
			</button>
		</div>
	</form>
</template>
