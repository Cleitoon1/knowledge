import Vue from 'vue'

export const baseApiUrl = 'http://localhost:5000/api'
export const userKey = '__knowledge_user'
export function showError(e) {
    if(e && e.response && e.response.data){
        if(e.response.data.notifications){
            e.response.data.notifications.forEach((value) => {
                Vue.toasted.global.defaultError({msg : value.message})
            })
        }
        else
            Vue.toasted.global.defaultError({msg : e.response.data})
    }
    else if (typeof e === 'string')
        Vue.toasted.global.defaultError(e)
    else
        Vue.toasted.global.defaultError()
}

export default { baseApiUrl, showError, userKey }